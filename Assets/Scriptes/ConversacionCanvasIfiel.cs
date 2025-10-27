using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConversacionCanvasIfiel : MonoBehaviour
{
    [Header("Imágenes de la conversación")]
    public Sprite[] imagenes; // Arreglo de imágenes para mostrar
    public Image displayImage; // UI Image donde se mostrarán las imágenes

    [Header("Audio")]
    public AudioSource audioFinal; // Sonido que se reproduce en la última imagen

    [Header("Opciones")]
    public GameObject canvasNarrativa; // Canvas principal de narrativa
    public float tiempoEntreImagenes = 2f; // Tiempo en segundos entre cada imagen

    private int indiceActual = 0;

    void Start()
    {
        if (imagenes.Length > 0 && displayImage != null)
        {
            displayImage.sprite = imagenes[0];
            indiceActual = 0;
            StartCoroutine(AvanzarAutomaticamente());
        }

        if (canvasNarrativa != null)
            canvasNarrativa.SetActive(true);

        if (audioFinal != null)
            audioFinal.Stop();
    }

    private IEnumerator AvanzarAutomaticamente()
    {
        while (indiceActual < imagenes.Length)
        {
            // Mostrar imagen actual
            if (displayImage != null)
                displayImage.sprite = imagenes[indiceActual];

            // Si es la última imagen, reproducir el audio
            if (indiceActual == imagenes.Length - 1 && audioFinal != null)
                audioFinal.Play();

            indiceActual++;
            yield return new WaitForSeconds(tiempoEntreImagenes);
        }

        // Terminar conversación
        CerrarCanvas();
    }

    // Cerrar el canvas de conversación
    private void CerrarCanvas()
    {
        if (canvasNarrativa != null)
            canvasNarrativa.SetActive(false);

        gameObject.SetActive(false);
    }
}
