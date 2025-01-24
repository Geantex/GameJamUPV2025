using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed; // Velocidad base de movimiento
    public float sprintMultiplier; // Multiplicador de velocidad al esprintar
    public float jumpForce; // Fuerza del salto
    public float mouseSensitivity; // Sensibilidad del ratón
    public float gravity; // Gravedad ajustable desde el inspector
    public Transform head; // Cámara (o cabeza) para rotar hacia arriba/abajo
    public float airControlMultiplier = 0.5f; // Control direccional en el aire (0 = sin control, 1 = igual que en el suelo)

    private CharacterController characterController; // Referencia al Character Controller
    private Vector3 currentMomentum = Vector3.zero; // Momentum acumulado del jugador
    private float verticalRotation = 0f; // Rotación vertical de la cabeza
    private float verticalVelocity = 0f; // Velocidad vertical (para saltos y gravedad)
    private bool isCursorLocked = true; // Estado del cursor bloqueado
    private int airJumpsLeft = 0; // Contador de saltos en el aire disponibles
    public int temporalAirJumpsLeft = 0; // Contador de saltos en el aire temporales

    private reliquiasEquipadas reliquias; // Referencia al script de reliquias

    // Variables para el "bobbing" (balanceo de la cabeza cuando andas)
    public float BOB_FREQ; // Frecuencia del balanceo
    public float BOB_AMP; // Amplitud del balanceo vertical
    public float BOB_SIDE_AMP; // Amplitud del balanceo hacia los lados
    private float t_bob; // Temporizador para el efecto de bobbing

    // Variables de Coyote Time y Buffer de Salto
    public float COYOTE_TIME; // Cuánto tiempo puede saltar después de dejar el suelo (en frames)
    private float coyote_variable = 0.0f; // Contador para el Coyote Time
    public float jumpBufferTime = 0.2f; // Tiempo máximo para el buffer de salto
    private float jumpBufferCounter = 0.0f; // Temporizador para el buffer de salto

    void Start()
    {
        LockCursor(); // Bloquea el cursor al iniciar el juego
        characterController = GetComponent<CharacterController>();
        reliquias = GetComponent<reliquiasEquipadas>(); // Obtén el componente de reliquias
        coyote_variable = COYOTE_TIME; // Inicializa el contador de Coyote Time
    }

    void Update()
    {
        // Manejo del cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isCursorLocked)
            {
                UnlockCursor(); // Liberar cursor
            }
            else
            {
                LockCursor(); // Bloquear cursor
            }
        }

        // Si el cursor no está bloqueado, no permitir movimiento o rotación
        if (!isCursorLocked) return;

        // Rotación con el ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotar la cápsula hacia los lados (horizontal)
        transform.Rotate(Vector3.up * mouseX);

        // Rotar la cabeza hacia arriba y abajo (vertical)
        if (head != null)
        {
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Limitar el ángulo vertical
            head.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }

        // Movimiento horizontal con teclado
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Crear el vector de movimiento y normalizarlo si es necesario
        Vector3 movementInput = new Vector3(moveHorizontal, 0, moveVertical);
        if (movementInput.magnitude > 1f)
        {
            movementInput.Normalize(); // Normalizar para evitar que el movimiento diagonal sea más rápido
        }

        // Determinar velocidad (esprintar si está desbloqueado y presionas Shift)
        float currentSpeed = speed;
        if (reliquias.esprintar && Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= sprintMultiplier;
        }

        // Cálculo de movimiento en el aire o en el suelo
        Vector3 desiredMovement = transform.TransformDirection(movementInput) * currentSpeed;



        // Gravedad y salto
        if (characterController.isGrounded)
        {
            verticalVelocity = -1f; // Asegurarse de que no flote al estar en el suelo
            airJumpsLeft = reliquias.saltoAereo; // Restablecer los saltos en el aire
            coyote_variable = COYOTE_TIME; // Reiniciar el contador de Coyote Time
        }
        else
        {
            // Reducir el contador de Coyote Time cuando no esté en el suelo
            if (coyote_variable > 0.0f)
            {
                //Debug.Log("Coyote Time: " + coyote_variable);
                coyote_variable -= Time.deltaTime * 100; // Reducir gradualmente el tiempo
            }
        }
        if (characterController.isGrounded)
        {
            // Movimiento normal en el suelo
            currentMomentum = desiredMovement;

            // Manejo de salto continuo y buffer
            if (jumpBufferCounter > 0)
            {
                verticalVelocity = jumpForce;
                coyote_variable = 0.0f; // Desactivar el Coyote Time
                jumpBufferCounter = 0.0f; // Resetea el buffer
            }
        }
        else
        {
            // Reducir la capacidad de cambiar de dirección en el aire
            currentMomentum = Vector3.Lerp(currentMomentum, desiredMovement, airControlMultiplier * Time.deltaTime);

            // Reducir el contador del buffer de salto
            if (jumpBufferCounter > 0)
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            verticalVelocity += gravity * Time.deltaTime;
        }
        // Entrada de salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (characterController.isGrounded)
            {
                coyote_variable = 0.0f; // Desactivar el Coyote Time
                verticalVelocity = jumpForce;
            }
            else if (coyote_variable > 0.0f)
            {
                //Debug.Log("Coyote Jump!");
                verticalVelocity = jumpForce;
                coyote_variable = 0.0f; // Desactivar el Coyote Time
            }
            else if (airJumpsLeft > 0)
            {
                //Debug.Log("Permanent Air Jump!");
                verticalVelocity = jumpForce;
                coyote_variable = 0.0f; // Desactivar el Coyote Time
                airJumpsLeft--;
            } 
            else if (temporalAirJumpsLeft > 0)
            {
                //Debug.Log("Temporal Air Jump!");
                verticalVelocity = jumpForce;
                coyote_variable = 0.0f; // Desactivar el Coyote Time
                temporalAirJumpsLeft--;
            }
            else
            {
                jumpBufferCounter = jumpBufferTime; // Activar el buffer si no se puede saltar ahora
            }
        }


        // Aplicar gravedad al movimiento
        Vector3 movement = currentMomentum;
        movement.y = verticalVelocity;

        // Mover al personaje
        characterController.Move(movement * Time.deltaTime);

        // HeadBobbing
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            // Calcular frecuencia y amplitud en función de la velocidad
            float bobFreq = BOB_FREQ * (currentSpeed / speed);

            // Incrementa el temporizador basado en la frecuencia
            t_bob += Time.deltaTime * bobFreq;

            // Desplazamiento vertical (solo en el suelo)
            float verticalOffset = characterController.isGrounded ? Mathf.Sin(t_bob) * BOB_AMP : 0f;

            // Desplazamiento lateral (siempre activo)
            float horizontalOffset = Mathf.Cos(t_bob / 2) * BOB_SIDE_AMP;

            // Aplica el desplazamiento relativo al eje Y y X
            head.localPosition = new Vector3(horizontalOffset, 0.8f + verticalOffset, head.localPosition.z);
        }
    }

    // Método para bloquear el cursor
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true;
    }

    // Método para liberar el cursor
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
    }
}
