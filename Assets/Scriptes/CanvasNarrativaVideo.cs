using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro; // 👈 Importante para usar TextMeshPro

public class CanvasNarrativaVideo : MonoBehaviour
{
    [Header("Video")]
    public VideoPlayer videoPlayer; // Componente del video
    public GameObject panelVideo;   // Panel donde se muestra el video

    [Header("Elementos UI post-video")]
    public Button botonSi;
    public Button botonNo;
    public TMP_Text textoMensaje; // 👈 Ahora usa TextMeshPro

    [Header("Escenas destino")]
    public string escenaSi;
    public string escenaNo;

    private void Start()
    {
        // Asegurar que los botones y el texto estén ocultos al inicio
        if (botonSi != null) botonSi.gameObject.SetActive(false);
        if (botonNo != null) botonNo.gameObject.SetActive(false);
        if (textoMensaje != null) textoMensaje.gameObject.SetActive(false);

        // Escuchar cuando el video termine
        if (videoPlayer != null)
            videoPlayer.loopPointReached += VideoTerminado;

        // Asignar eventos a los botones
        if (botonSi != null) botonSi.onClick.AddListener(OpcionSi);
        if (botonNo != null) botonNo.onClick.AddListener(OpcionNo);
    }

    private void OnEnable()
    {
        // Cuando el Canvas se active, iniciar el video
        if (videoPlayer != null)
        {
            panelVideo.SetActive(true);
            videoPlayer.Play();
        }
    }

    void VideoTerminado(VideoPlayer vp)
    {
        // Ocultar el panel de video
        if (panelVideo != null)
            //panelVideo.SetActive(false);

        // Mostrar botones y texto
        if (botonSi != null) botonSi.gameObject.SetActive(true);
        if (botonNo != null) botonNo.gameObject.SetActive(true);
        if (textoMensaje != null) textoMensaje.gameObject.SetActive(true);
    }

    void OpcionSi()
    {
        if (!string.IsNullOrEmpty(escenaSi))
            SceneManager.LoadScene(escenaSi);
    }

    void OpcionNo()
    {
        if (!string.IsNullOrEmpty(escenaNo))
            SceneManager.LoadScene(escenaNo);
    }
}
