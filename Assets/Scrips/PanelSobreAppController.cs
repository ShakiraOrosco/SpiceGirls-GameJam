using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PanelSobreAppController : MonoBehaviour
{
    public Button botonHistoria;
    public Button botonEquipo;
    public Button botonVolver;

    public GameObject panelHistoria;
    public GameObject panelEquipo;

    public Button botonCerrarHistoria;
    public Button botonCerrarEquipo;

    public VideoPlayer videoLoop;     // Video en loop del SobreApp
    public GameObject panelSalida;    // Panel de salida
    public VideoPlayer videoSalida;   // Video de salida
    public string nombreEscenaDestino = "Menu";

    private void Start()
    {
        panelHistoria.SetActive(false);
        panelEquipo.SetActive(false);
        panelSalida.SetActive(false);

        botonHistoria.onClick.AddListener(() => panelHistoria.SetActive(true));
        botonEquipo.onClick.AddListener(() => panelEquipo.SetActive(true));
        botonCerrarHistoria.onClick.AddListener(() => panelHistoria.SetActive(false));
        botonCerrarEquipo.onClick.AddListener(() => panelEquipo.SetActive(false));

        botonVolver.onClick.AddListener(ActivarSalida);
    }

    private void ActivarSalida()
    {
        // Detener video loop del SobreApp
        if (videoLoop != null)
            videoLoop.Stop();

        // Ocultar panel SobreApp
        gameObject.SetActive(false);

        // Activar panel de salida y reproducir su video
        panelSalida.SetActive(true);
        if (videoSalida != null)
        {
            videoSalida.isLooping = false;
            videoSalida.Play();
            videoSalida.loopPointReached += OnVideoSalidaTerminado;
        }
        else
        {
            // Si no hay video, cargar escena directamente
            CargarEscena();
        }
    }

    private void OnVideoSalidaTerminado(VideoPlayer vp)
    {
        videoSalida.Stop();
        panelSalida.SetActive(false);
        CargarEscena();
    }

    private void CargarEscena()
    {
        SceneManager.LoadScene(nombreEscenaDestino);
    }
}
