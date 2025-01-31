using System.Collections;
using UnityEngine;

public class LaserMagico : MonoBehaviour
{
    [SerializeField] private Transform puntoDeDisparo; // Punto de inicio del láser (la varita)
    [SerializeField] private GameObject cilindroPrefab; // Prefab del cilindro que usaremos como láser
    [SerializeField] private Camera camaraPrincipal; // Cámara principal para el raycast
    [SerializeField] private float velocidadLaser = 50f; // Velocidad del láser avanzando hacia el punto final
    [SerializeField] private float grosorInicial = 0.2f; // Grosor inicial del láser
    [SerializeField] private float velocidadCambioGrosor = 2f; // Velocidad a la que el grosor cambia
    [SerializeField] private Color colorLaser = Color.blue; // Color del láser
    //[SerializeField] private float distanciaMaximaSinImpacto = 100f; // Distancia máxima si no hay impacto
    [SerializeField] private float velocidadSeno = 15f;
    [SerializeField] private float amplitudSenoGrosor = 0.02f;
    [SerializeField] private float amplitudSenoColor = 0.1f;

    private GameObject laserCilindro; // Instancia del cilindro
    private ParticleSystem sistemaDeParticulas; // Sistema de partículas del láser
    private bool disparando = false; // Si el láser está en acción
    private Coroutine reducirGrosorCoroutine; // Referencia a la corrutina de reducción
    private Coroutine particulasCoroutine; // Referencia a la corrutina del sistema de partículas
    private Coroutine efectoSenoCoroutine; // Corrutina para el efecto seno
    private float grosorActual;
    private float distanciaActual = 0f; // Distancia progresiva del láser
    private Material laserMaterial; // Material del cilindro

    [Header("Configuración Sonidos")]
    [SerializeField] public AdministradorAudio administradorAudio;
    void Start()
    {
        administradorAudio = GameObject.FindGameObjectWithTag("administradorAudio").GetComponent<AdministradorAudio>();

        // Instanciar el cilindro y desactivarlo inicialmente
        laserCilindro = Instantiate(cilindroPrefab);
        laserCilindro.SetActive(false);

        // Aqui le metemos las stats al laser POR CIERTO AHORA HAY QUE REINICIAR MODO PLAY PARA VER MODIFICACIONES EN EL LASER HEHE OOPS
        statsLaser originalStats = GetComponent<statsLaser>();
        if (originalStats != null)
        {
            statsLaser copiarStats = laserCilindro.AddComponent<statsLaser>();   
            copiarStats.damagePerFrame = originalStats.damagePerFrame;
            copiarStats.burnPerFrame = originalStats.burnPerFrame;
            copiarStats.extraDamageOnBurned = originalStats.extraDamageOnBurned;
            copiarStats.efectoDeMuerte = originalStats.efectoDeMuerte;
        }
        // Configurar el material del cilindro
        Renderer renderer = laserCilindro.GetComponent<Renderer>();
        if (renderer != null)
        {
            laserMaterial = renderer.material;
            laserMaterial.color = colorLaser;
        }

        // Buscar automáticamente el sistema de partículas dentro del prefab del láser
        sistemaDeParticulas = laserCilindro.GetComponentInChildren<ParticleSystem>();
        if (sistemaDeParticulas == null)
        {
            Debug.LogError("No se encontró un sistema de partículas en el prefab del láser.");
        }

        // Inicializar el grosor actual
        grosorActual = grosorInicial;

    }

    void Update()
    {
        // Detectar click izquierdo para disparar
        if (Input.GetMouseButtonDown(1))
        {
            OnDisparar();
        }

        if (Input.GetMouseButtonUp(1))
        {
            OnDejarDeDisparar();
        }
    }

    private void OnDisparar()
    {
        playerController playerController = GetComponentInParent<playerController>();
        if (playerController.isCursorLocked == false) return;
        if (!disparando) // Solo activa si no está disparando
        {
            disparando = true; // Cambia el estado

            if(administradorAudio == null){
                Debug.Log("Te tengo bastardO!");
                administradorAudio = GameObject.FindGameObjectWithTag("administradorAudio").GetComponent<AdministradorAudio>();
            }

            // Reproducir el sonido del láser inicial
            if (administradorAudio != null)
            {
                administradorAudio.ReproducirSonidoDisparoLaser();
            }

            if (reducirGrosorCoroutine != null)
            {
                StopCoroutine(reducirGrosorCoroutine);
                reducirGrosorCoroutine = null;
            }
            StartCoroutine(ActivarLaser());
        }
    }



    private void OnDejarDeDisparar()
    {
        if (disparando) // Solo desactiva si está disparando
        {
            disparando = false; // Cambia el estado

            // Detener el sonido del láser en loop
            if (administradorAudio != null)
            {
                Debug.Log("Dejar Disparar Laser");
                administradorAudio.DetenerSonidoDisparoLaser();
            }

            if (particulasCoroutine != null)
            {
                StopCoroutine(particulasCoroutine);
                particulasCoroutine = null;
            }
            if (efectoSenoCoroutine != null)
            {
                StopCoroutine(efectoSenoCoroutine);
                efectoSenoCoroutine = null;
            }
            DetenerParticulas();
            if (reducirGrosorCoroutine == null) // Reinicia el ciclo de grosor
            {
                reducirGrosorCoroutine = StartCoroutine(ReducirGrosor());
            }
        }
    }

    private IEnumerator ActivarLaser()
    {
        disparando = true;

        // Activar el cilindro
        laserCilindro.SetActive(true);

        // Reiniciar el grosor y la distancia del láser
        grosorActual = grosorInicial;
        distanciaActual = 0f;

        // Iniciar la corrutina para activar las partículas
        if (particulasCoroutine == null)
        {
            particulasCoroutine = StartCoroutine(EsperarYActivarParticulas(0.25f));
        }

        // Iniciar la corrutina para el efecto seno
        if (efectoSenoCoroutine == null)
        {
            efectoSenoCoroutine = StartCoroutine(EfectoSeno());
        }

        girar_municion girarMunicion = GetComponent<girar_municion>();

        while (disparando)
        {
            // Calcular el punto final del láser
            Vector3 puntoFinal;
            Vector3 direccion = camaraPrincipal.transform.forward;
            Ray ray = new Ray(puntoDeDisparo.position, direccion);
            RaycastHit hit;

            // Intentar avanzar el láser hacia el destino con velocidad constante
            distanciaActual += velocidadLaser * Time.deltaTime;

            if (Physics.Raycast(ray, out hit, distanciaActual))
            {
                puntoFinal = hit.point; // Detener el láser en el punto de impacto
                distanciaActual = Vector3.Distance(puntoDeDisparo.position, hit.point); // Ajustar la distancia al impacto
            }
            else
            {
                // Si no hay impacto, avanzar hacia el punto máximo
                puntoFinal = puntoDeDisparo.position + direccion * distanciaActual;
            }
            girarMunicion.BoostSpeed();
            // Actualizar el cilindro para que toque exactamente el punto final
            ActualizarCilindro(puntoDeDisparo.position, puntoFinal);

            yield return null;
        }

        disparando = false;
    }



    private IEnumerator EfectoSeno()
    {
        float tiempo = 0f;

        while (disparando)
        {
            tiempo += Time.deltaTime * velocidadSeno; // Ajustar la velocidad del efecto seno

            // Variación del grosor del láser
            float variacionGrosor = Mathf.Sin(tiempo) * amplitudSenoGrosor; 
            Vector3 nuevaEscala = new Vector3(grosorActual + variacionGrosor, laserCilindro.transform.localScale.y, grosorActual + variacionGrosor);

            // Aplicar la nueva escala
            laserCilindro.transform.localScale = nuevaEscala;

            // Variación del color del láser
            float intensidadColor = Mathf.Sin(tiempo) * amplitudSenoColor; // Ajustar intensidad del color
            Color nuevoColor = colorLaser * (1f + intensidadColor); // Cambiar brillo del color
            laserMaterial.color = nuevoColor;
            yield return null;
        }
    }

    private IEnumerator EsperarYActivarParticulas(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);

        // Activar las partículas solo si el láser aún está activo
        if (disparando && sistemaDeParticulas != null)
        {
            var emission = sistemaDeParticulas.emission;
            emission.enabled = true;
        }
    }

    private void DetenerParticulas()
    {
        if (sistemaDeParticulas != null)
        {
            var emission = sistemaDeParticulas.emission;
            emission.enabled = false;
        }
    }

    private IEnumerator ReducirGrosor()
    {
        while (grosorActual > 0.01f)
        {
            grosorActual -= velocidadCambioGrosor * Time.deltaTime;
            grosorActual = Mathf.Max(grosorActual, 0.01f);

            // Actualizar el grosor del cilindro
            laserCilindro.transform.localScale = new Vector3(grosorActual, laserCilindro.transform.localScale.y, grosorActual);

            yield return null;
        }

        laserCilindro.SetActive(false);
    }

    private void ActualizarCilindro(Vector3 inicio, Vector3 fin)
    {
        // Calcular la dirección del láser
        Vector3 direccion = fin - inicio;

        // Extender el láser 0.05 unidades más allá del punto final
        //fin += direccion.normalized * 0.05f;

        // Calcular la posición del centro entre el inicio y el fin
        Vector3 posicionMedia = (inicio + fin) / 2f;

        // Calcular la distancia entre el inicio y el fin
        float distancia = Vector3.Distance(inicio, fin);

        // Ajustar la posición del cilindro
        laserCilindro.transform.position = posicionMedia;

        // Orientar el cilindro hacia el objetivo
        laserCilindro.transform.rotation = Quaternion.LookRotation(direccion);

        // Corregir la orientación del cilindro (eje Y -> eje Z)
        laserCilindro.transform.Rotate(90f, 0f, 0f);

        // Mantener la escala en los ejes x y z, solo ajustar el eje y (longitud)
        Vector3 escalaActual = laserCilindro.transform.localScale;
        laserCilindro.transform.localScale = new Vector3(escalaActual.x, distancia / 2f, escalaActual.z);
    }
    public void DesactivarLaserPorCambioDeHechizo()
    {
        // Detener el sonido del láser en loop
        /*if (administradorAudio != null)
        {
            Debug.Log("Desactivar Laser");
            administradorAudio.DetenerSonidoLaser();
        }*/

        // Detener las partículas y corrutinas asociadas
        if (particulasCoroutine != null)
        {
            StopCoroutine(particulasCoroutine);
            particulasCoroutine = null;
        }

        if (efectoSenoCoroutine != null)
        {
            StopCoroutine(efectoSenoCoroutine);
            efectoSenoCoroutine = null;
        }

        // Detener las partículas activas, si existen
        DetenerParticulas();

        // Desactivar el cilindro del láser de manera gradual
        if (reducirGrosorCoroutine == null) // Solo si no está ya desactivándose
        {
            reducirGrosorCoroutine = StartCoroutine(ReducirGrosor());
        }

        // Desactivar el script después de desactivar el láser
        StartCoroutine(DesactivarScriptAlFinal());
    }

    // Corrutina para desactivar el script después de que el láser se reduzca
    private IEnumerator DesactivarScriptAlFinal()
    {
        // Esperar a que el láser termine de reducirse
        while (laserCilindro.activeSelf)
        {
            yield return null;
        }

        // Desactivar el script
        disparando = false;
        this.enabled = false;
    }
}
