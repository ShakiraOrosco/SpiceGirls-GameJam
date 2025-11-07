using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorPuertas : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelEleccion; // Panel "¿Qué vida quieres escoger?"
    
    [Header("Puertas")]
    public GameObject puertaIzquierda;
    public GameObject puertaCentro;
    public GameObject puertaDerecha;
    
    [Header("Luces de Puertas")]
    public Light luzPuertaIzquierda;
    public Light luzPuertaCentro;
    public Light luzPuertaDerecha;
    
    [Header("Animación de Puertas")]
    public float velocidadApertura = 2f;
    public float anguloApertura = -90f; // Negativo para abrir hacia adentro
    
    [Header("Escenas")]
    public string escenaNivel1 = "Nivel1"; // Nombre de la escena para puerta izquierda
    public string escenaNivel2 = "Nivel2"; // Nombre de la escena para puerta centro
    public string escenaNivel3 = "Nivel3"; // Nombre de la escena para puerta derecha
    
    [Header("Configuración")]
    public float tiempoAntesDeCargar = 2f; // Tiempo que espera antes de cambiar de escena
    
    [Header("Sonidos")]
    public AudioClip sonidoPuertaAbriendose; // Sonido al abrir la puerta
    public AudioClip sonidoTransicion; // Sonido al cambiar de escena
    public float volumenSonido = 1f;
    
    private bool panelActivo = false;
    private bool puertaSeleccionada = false;
    private TeresaMovimiento teresaMovimiento;
    private AudioSource audioSource;

    void Start()
    {
        // Desactivar panel al inicio
        if (panelEleccion != null)
            panelEleccion.SetActive(false);
        
        // Configurar y desactivar luces al inicio
        ConfigurarLuz(luzPuertaIzquierda);
        ConfigurarLuz(luzPuertaCentro);
        ConfigurarLuz(luzPuertaDerecha);
        
        // Crear AudioSource si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    void ConfigurarLuz(Light luz)
    {
        if (luz != null)
        {
            luz.intensity = 0f;
            luz.enabled = true; // Mantener habilitada pero con intensidad 0
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Detectar cuando Teresa entra en el trigger
        if (other.CompareTag("Player") && !panelActivo && !puertaSeleccionada)
        {
            teresaMovimiento = other.GetComponent<TeresaMovimiento>();
            MostrarPanelEleccion();
        }
    }

    void MostrarPanelEleccion()
    {
        panelActivo = true;
        
        // Activar panel
        if (panelEleccion != null)
            panelEleccion.SetActive(true);
        
        // Mostrar y desbloquear cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Congelar el Rigidbody pero NO desactivar el script
        if (teresaMovimiento != null)
        {
            Rigidbody rb = teresaMovimiento.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.constraints = RigidbodyConstraints.FreezeAll; // Congelar todo
            }
        }
    }

    // Método para el botón de Puerta Izquierda
    public void SeleccionarPuertaIzquierda()
    {
        if (!puertaSeleccionada)
        {
            puertaSeleccionada = true;
            StartCoroutine(AbrirPuertaYCargarEscena(puertaIzquierda, luzPuertaIzquierda, escenaNivel1));
        }
    }

    // Método para el botón de Puerta Centro
    public void SeleccionarPuertaCentro()
    {
        if (!puertaSeleccionada)
        {
            puertaSeleccionada = true;
            StartCoroutine(AbrirPuertaYCargarEscena(puertaCentro, luzPuertaCentro, escenaNivel2));
        }
    }

    // Método para el botón de Puerta Derecha
    public void SeleccionarPuertaDerecha()
    {
        if (!puertaSeleccionada)
        {
            puertaSeleccionada = true;
            StartCoroutine(AbrirPuertaYCargarEscena(puertaDerecha, luzPuertaDerecha, escenaNivel3));
        }
    }

    IEnumerator AbrirPuertaYCargarEscena(GameObject puerta, Light luzPuerta, string nombreEscena)
    {
        // Ocultar panel
        if (panelEleccion != null)
            panelEleccion.SetActive(false);
        
        // Descongelar el Rigidbody
        if (teresaMovimiento != null)
        {
            Rigidbody rb = teresaMovimiento.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
        }
        
        // Volver a bloquear el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // Activar y animar luz de la puerta
        if (luzPuerta != null)
        {
            Debug.Log("Activando luz de la puerta: " + luzPuerta.name);
            luzPuerta.enabled = true;
            luzPuerta.intensity = 0f;
            
            // Aumentar intensidad gradualmente
            float tiempoLuz = 0f;
            float intensidadMaxima = 90f; // Aumentado para ser más visible
            
            while (tiempoLuz < 1f)
            {
                tiempoLuz += Time.deltaTime * 1.5f; // Más lento para ver el efecto
                luzPuerta.intensity = Mathf.Lerp(0f, intensidadMaxima, tiempoLuz);
                yield return null;
            }
            
            luzPuerta.intensity = intensidadMaxima;
            Debug.Log("Luz activada con intensidad: " + luzPuerta.intensity);
        }
        else
        {
            Debug.LogWarning("No hay luz asignada para esta puerta!");
        }
        
        // Abrir puerta (rotación)
        if (puerta != null)
        {
            Debug.Log("Abriendo puerta: " + puerta.name);
            Quaternion rotacionInicial = puerta.transform.rotation;
            Quaternion rotacionFinal = rotacionInicial * Quaternion.Euler(0, anguloApertura, 0);
            
            float tiempo = 0f;
            while (tiempo < 1f)
            {
                tiempo += Time.deltaTime * velocidadApertura;
                puerta.transform.rotation = Quaternion.Lerp(rotacionInicial, rotacionFinal, tiempo);
                yield return null;
            }
            
            Debug.Log("Puerta abierta completamente");
        }
        
        // Esperar antes de cargar la escena
        Debug.Log("Esperando " + tiempoAntesDeCargar + " segundos antes de cargar escena...");
        yield return new WaitForSeconds(tiempoAntesDeCargar);
        
        // Cargar la escena
        if (!string.IsNullOrEmpty(nombreEscena))
        {
            Debug.Log("Cargando escena: " + nombreEscena);
            SceneManager.LoadScene(nombreEscena);
        }
        else
        {
            Debug.LogWarning("No hay nombre de escena asignado!");
        }
    }
}