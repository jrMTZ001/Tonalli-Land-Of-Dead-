using UnityEngine;

public class MonedaManager : MonoBehaviour
{
    public static MonedaManager Instance;  // Instancia del singleton

    public int monedasTotales = 0;  // Total de monedas recolectadas

    private void Awake()
    {
        // Asegurarse de que solo haya una instancia de MonedaManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para agregar monedas al total
    public void AgregarMonedas(int cantidad)
    {
        monedasTotales += cantidad;
        Debug.Log("Monedas: " + monedasTotales);
    }

    // Método para obtener el total de monedas
    public int ObtenerMonedas()
    {
        return monedasTotales;
    }
}
