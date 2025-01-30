using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dificultad", menuName = "Dificultad/Configuración")]

public class DificultadCambios : ScriptableObject
{
    [Header("Dificultad")]
    [Tooltip("0 = fácil, 1 = normal, 2 = difícil")]
    [Range(0, 2)] public int dificultad = 1;

    [Header("Configuración de la dificultad del día")]
    public int enemyDamage;
    public float enemySpeed;
    public int startingMoney;

    [Header("Configuración de la dificultad de la noche")]
    public float tortosaSpeed;
    public int precioAntorcha;
}
