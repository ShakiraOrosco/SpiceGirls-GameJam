using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ConversacionCanvas : MonoBehaviour
{
    [Header("Imágenes de la conversación")]
    public Sprite[] imagenes;
    public Image displayImage;

    [Header("Audio")]
    public AudioSource audioFinal;

    [Header("Opciones")]
    public GameObject canvasNarrativa;
    public float tiempoEntreImagenes = 2f;

    [Header("NPC")]
    public NPCMovement npcInfiel; // Asignar en inspector

    [Header("Texto Post-Conversación")]
    public TextMeshProUGUI textoFinal; // Asignar en inspector
    public string nuevoTexto = "Encuentra el celular";

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

            // Reproducir audio en la última imagen
            if (indiceActual == imagenes.Length - 1 && audioFinal != null)
                audioFinal.Play();

            indiceActual++;
            yield return new WaitForSeconds(tiempoEntreImagenes);
        }

        // Terminar conversación
        CerrarCanvas();
    }

    private void CerrarCanvas()
    {
        // Ocultar canvas de conversación
        if (canvasNarrativa != null)
            canvasNarrativa.SetActive(false);

        gameObject.SetActive(false);

        // Activar NPC
        if (npcInfiel != null)
            npcInfiel.enabled = true;

        // Cambiar texto
        if (textoFinal != null)
            textoFinal.text = nuevoTexto;
    }
}
