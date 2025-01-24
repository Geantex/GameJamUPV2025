using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    // Cantidad actual de dinero del jugador
    public int currentMoney;

    // Propiedad de solo lectura por si quieres exponer el dinero p�blicamente
    public int CurrentMoney
    {
        get { return currentMoney; }
    }

    // Funci�n para a�adir dinero
    public void AddMoney(int amount)
    {
        currentMoney += amount;
        Debug.Log("Se han a�adido " + amount + " monedas. Total ahora: " + currentMoney);
    }

    // Funci�n para quitar dinero
    // Devuelve true si se ha podido quitar la cantidad indicada, false en caso contrario
    public bool SpendMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            Debug.Log("Se han gastado " + amount + " monedas. Total ahora: " + currentMoney);
            return true;
        }
        else
        {
            Debug.Log("No hay suficiente dinero para gastar " + amount + " monedas.");
            return false;
        }
    }
}
