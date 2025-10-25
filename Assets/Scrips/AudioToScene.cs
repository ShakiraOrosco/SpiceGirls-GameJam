using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioToSceneSoloAsignado : MonoBehaviour
{
    [Header("Audio de la última escena (opcional)")]
    public AudioSource audioUltima;  

    [Header("Nombre de la escena destino")]
    public string nombreEscenaDestino = "Menu";

    private void Start()
    {
        if (audioUltima != null && audioUltima.clip != null)
        {
            audioUltima.Play();
            StartCoroutine(EsperarFinAudio());
        }
        else
        {
            // Si no hay audio, cargar inmediatamente
            CargarMenu();
        }
    }

    private System.Collections.IEnumerator EsperarFinAudio()
    {
        while (audioUltima.isPlaying)
            yield return null;

        CargarMenu();
    }

    private void CargarMenu()
    {
        SceneManager.LoadScene(nombreEscenaDestino);
    }
}
