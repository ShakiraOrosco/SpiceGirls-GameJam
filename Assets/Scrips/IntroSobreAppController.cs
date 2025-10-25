using UnityEngine;
using UnityEngine.Video;

public class IntroSobreAppController : MonoBehaviour
{
   public GameObject panelIntro;
    public VideoPlayer videoIntro;

    public GameObject panelSobreApp;     // Panel de SobreApp
    public VideoPlayer videoLoopSobreApp; // Video loop del SobreApp

    private void Start()
    {
        // Asegurar que SobreApp no esté activo
        panelSobreApp.SetActive(false);
        if (videoLoopSobreApp != null)
            videoLoopSobreApp.Stop();

        // Reproducir solo el video de intro
        videoIntro.isLooping = false;
        videoIntro.Play();
        videoIntro.loopPointReached += OnIntroTerminada;
    }

    private void OnIntroTerminada(VideoPlayer vp)
    {
        // Detener el video de intro y ocultar panel
        videoIntro.Stop();
        panelIntro.SetActive(false);

        // Activar panel SobreApp y reproducir su video en loop
        panelSobreApp.SetActive(true);
        if (videoLoopSobreApp != null)
        {
            videoLoopSobreApp.isLooping = true;
            videoLoopSobreApp.Play();
        }
    }
}
