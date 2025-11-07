using System.Collections;
using UnityEngine;

public class PuertaController : MonoBehaviour
{
    [Header("Configuración de Apertura")]
    public Vector3 rotacionAbierta = new Vector3(0, 90, 0);
    public float velocidadApertura = 2f;
    public float tiempoParaCerrar = 2f; // Tiempo antes de cerrar

    [Header("Audio")]
    public AudioClip sonidoAbrir;
    public AudioClip sonidoCerrar;
    private AudioSource audioSource;

    [Header("Canvas UI")]
    public GameObject canvas; // Canvas que se activa/desactiva

    [Header("Zona de Cierre")]
    public GameObject zonaCierre; // GameObject con trigger para detectar cuando Teresa entra

    private bool estaCerrada = true;
    private bool estaAbriendo = false;
    private bool estaCerrando = false;
    private Quaternion rotacionInicial;

    void Start()
    {
        rotacionInicial = transform.localRotation;
        
        // Configurar AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;

        // Desactivar canvas al inicio
        if (canvas != null)
            canvas.SetActive(false);
    }

    public void AbrirPuerta()
    {
        if (estaCerrada && !estaAbriendo)
        {
            // Reproducir sonido de apertura
            if (sonidoAbrir != null && audioSource != null)
            {
                audioSource.clip = sonidoAbrir;
                audioSource.Play();
            }

            // Activar canvas
            if (canvas != null)
                canvas.SetActive(true);
            
            StartCoroutine(AnimarApertura());
        }
    }

    IEnumerator AnimarApertura()
    {
        estaAbriendo = true;
        float tiempo = 0f;

        Quaternion rotacionFinal = rotacionInicial * Quaternion.Euler(rotacionAbierta);
        
        while (tiempo < 1f)
        {
            tiempo += Time.deltaTime * velocidadApertura;
            transform.localRotation = Quaternion.Lerp(rotacionInicial, rotacionFinal, tiempo);
            yield return null;
        }

        estaCerrada = false;
        estaAbriendo = false;

        // Activar el trigger de zona de cierre
        if (zonaCierre != null)
            zonaCierre.SetActive(true);
    }

    public void CerrarPuertaAutomaticamente()
    {
        if (!estaCerrada && !estaCerrando)
        {
            StartCoroutine(CerrarConRetraso());
        }
    }

    IEnumerator CerrarConRetraso()
    {
        yield return new WaitForSeconds(tiempoParaCerrar);
        
        // Reproducir sonido de cerradura
        if (sonidoCerrar != null && audioSource != null)
        {
            audioSource.clip = sonidoCerrar;
            audioSource.Play();
        }

        StartCoroutine(AnimarCierre());
    }

    IEnumerator AnimarCierre()
    {
        estaCerrando = true;
        float tiempo = 0f;
        Quaternion rotacionActual = transform.localRotation;
        
        while (tiempo < 1f)
        {
            tiempo += Time.deltaTime * velocidadApertura;
            transform.localRotation = Quaternion.Lerp(rotacionActual, rotacionInicial, tiempo);
            yield return null;
        }

        estaCerrada = true;
        estaCerrando = false;

        // Desactivar canvas
        if (canvas != null)
            canvas.SetActive(false);

        // Desactivar zona de cierre
        if (zonaCierre != null)
            zonaCierre.SetActive(false);
    }
}