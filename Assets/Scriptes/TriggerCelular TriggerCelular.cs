using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TriggerCelular : MonoBehaviour
{
    [Header("Configuraciones")]
    public string escenaNarrativa = "NarrativaInfidelidad"; // Escena al leer los mensajes
    public string escenaMenu = "Menu"; // Escena a la que se regresa después de 10 segundos

    [Header("Referencias UI")]
    public Canvas canvasOpciones;       // Canvas principal (contiene el panel de opciones)
    public GameObject panelOpciones;    // Panel que contiene los botones
    public Canvas canvasNegacion;       // Canvas del mensaje de negación
    public GameObject panelNegacion;    // Panel dentro del canvasNegacion
    public TMP_Text textoNegacion;      // Texto TMP dentro del panelNegacion

    [Header("Audio")]
    public AudioSource audioSource;     // Fuente de audio para reproducir el sonido
    public AudioClip audioNegacion;     // Clip de audio que sonará al elegir "Dejar el celular"

    private void Start()
    {
        // Aseguramos que los canvas y paneles estén ocultos al inicio
        if (canvasOpciones != null) canvasOpciones.gameObject.SetActive(false);
        if (panelOpciones != null) panelOpciones.SetActive(false);
        if (canvasNegacion != null) canvasNegacion.gameObject.SetActive(false);
        if (panelNegacion != null) panelNegacion.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Mostrar el Canvas y el Panel de opciones
            if (canvasOpciones != null) canvasOpciones.gameObject.SetActive(true);
            if (panelOpciones != null) panelOpciones.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Ocultar el Canvas y Panel de opciones si el jugador se aleja
            if (canvasOpciones != null) canvasOpciones.gameObject.SetActive(false);
            if (panelOpciones != null) panelOpciones.SetActive(false);
        }
    }

    // 📱 Botón: Leer los mensajes
    public void LeerMensajes()
    {
        SceneManager.LoadScene(escenaNarrativa);
    }

    // 🚪 Botón: Dejar el celular
    public void DejarCelular()
    {
        // Ocultamos el panel de opciones
        if (canvasOpciones != null) canvasOpciones.gameObject.SetActive(false);
        if (panelOpciones != null) panelOpciones.SetActive(false);

        // Mostramos el panel de negación
        if (canvasNegacion != null) canvasNegacion.gameObject.SetActive(true);
        if (panelNegacion != null) panelNegacion.SetActive(true);

        if (textoNegacion != null)
        {
            textoNegacion.text = "Elegiste vivir en negación y tal vez te enteres de la verdad en un futuro... pero será tarde.";
        }

        // Oscurecer fondo si el panel tiene una imagen
        Image fondo = panelNegacion.GetComponent<Image>();
        if (fondo != null)
        {
            fondo.color = new Color(0, 0, 0, 0.7f); // negro semitransparente
        }

        // 🎵 Reproducir el audio
        if (audioSource != null && audioNegacion != null)
        {
            audioSource.clip = audioNegacion;
            audioSource.Play();
        }

        // ⏳ Iniciar la corrutina para volver al menú después de 10 segundos
        StartCoroutine(RegresarAlMenu());
    }

    // Corrutina para esperar 10 segundos y luego cargar la escena "Menu"
    private System.Collections.IEnumerator RegresarAlMenu()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(escenaMenu);
    }
}
