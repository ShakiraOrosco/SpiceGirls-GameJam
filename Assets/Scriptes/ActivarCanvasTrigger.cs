using UnityEngine;

public class ActivarCanvasTrigger : MonoBehaviour
{
    [Header("Canvas que se activará")]
    public GameObject canvasActivar;

    [Header("Tag del jugador")]
    public string tagJugador = "Player";

    private void Start()
    {
        // Asegurarse de que el canvas esté desactivado al inicio
        if (canvasActivar != null)
            canvasActivar.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cuando el jugador entra al trigger
        if (other.CompareTag(tagJugador))
        {
            if (canvasActivar != null)
                canvasActivar.SetActive(true);
        }
    }
}
