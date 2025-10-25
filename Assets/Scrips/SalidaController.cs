using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SalidaController : MonoBehaviour
{
    [Header("Video de salida")]
    public VideoPlayer videoSalida;

    [Header("Nombre de la escena destino")]
    public string nombreEscenaDestino = "Menu"; // Cambia por el nombre real

    private void Start()
    {
        if (videoSalida != null)
        {
            videoSalida.isLooping = false;
            videoSalida.loopPointReached += OnVideoTerminado;
            videoSalida.Play();
        }
        else
        {
            Debug.LogWarning("⚠️ No hay VideoPlayer asignado en el PanelSalida. Cargando escena directamente...");
            CargarEscenaDestino();
        }
    }

    private void OnVideoTerminado(VideoPlayer vp)
    {
        videoSalida.Stop();
        gameObject.SetActive(false);
        CargarEscenaDestino();
    }

    private void CargarEscenaDestino()
    {
        SceneManager.LoadScene(nombreEscenaDestino);
    }
}
