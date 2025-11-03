using UnityEngine;

public class ZonaCierrePuertas : MonoBehaviour
{
    public PuertaController puertaIzquierda;
    public PuertaController puertaDerecha;

    void Start()
    {
        // Desactivar al inicio
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Cuando Teresa entra, cerrar ambas puertas
        if (other.CompareTag("Player"))
        {
            if (puertaIzquierda != null)
                puertaIzquierda.CerrarPuertaAutomaticamente();
            
            if (puertaDerecha != null)
                puertaDerecha.CerrarPuertaAutomaticamente();
        }
    }
}