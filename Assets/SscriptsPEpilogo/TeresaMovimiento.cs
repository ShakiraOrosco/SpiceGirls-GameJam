using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeresaMovimiento : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 12f;
    public float acceleration = 10f;
    public float deceleration = 15f;
    public float jumpForce = 5f;

    [Header("Rotación con Mouse")]
    public float mouseSensitivity = 3f;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 movement;
    private Vector3 currentVelocity;
    private bool isGrounded = true;

    [Header("Narrativa Bibliotecaria")]
    public GameObject panelInteraccion; // Panel "Presiona E para hablar"
    public GameObject narrativaCanvas;
    public Image imagenNarrativa; // Imagen donde se muestran las narrativas
    
    [Header("Configuración de Narrativas")]
    public Sprite[] narrativas; // Array de imágenes de narrativas
    public float tiempoEntreNarrativas =7f; // Tiempo entre cada narrativa
    
    private bool enRangoBibliotecaria = false;
    private bool narrativaActiva = false;
    private int narrativaActual = 0;
    private bool conversacionIniciada = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        if (rb != null)
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Bloquear y ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Desactivar UI al inicio
        if (panelInteraccion != null)
            panelInteraccion.SetActive(false);
        
        if (narrativaCanvas != null)
            narrativaCanvas.SetActive(false);
    }

    void Update()
    {
        // Si la narrativa está activa, no permitir movimiento ni rotación
        if (narrativaActiva)
            return;

        // Rotación con el mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        // Obtener input de movimiento
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calcular dirección de movimiento basada en la rotación actual
        Vector3 moveDirection = (transform.right * moveX + transform.forward * moveZ).normalized;

        // Suavizado del movimiento con aceleración/desaceleración
        if (moveDirection.magnitude > 0.1f)
        {
            currentVelocity = Vector3.Lerp(currentVelocity, moveDirection * moveSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        movement = currentVelocity;

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Animación
        if (animator != null)
            animator.SetBool("isWalking", currentVelocity.magnitude > 0.1f);

        // Interacción con la bibliotecaria
        if (enRangoBibliotecaria && Input.GetKeyDown(KeyCode.E) && !conversacionIniciada)
        {
            IniciarConversacion();
        }

        // Desbloquear cursor con ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void FixedUpdate()
    {
        if (rb != null && !narrativaActiva)
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;

        // Abrir puertas al chocar
        if (collision.gameObject.CompareTag("Puerta"))
        {
            PuertaController puerta = collision.gameObject.GetComponent<PuertaController>();
            if (puerta != null)
            {
                puerta.AbrirPuerta();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Detectar cuando está cerca de la bibliotecaria
        if (other.CompareTag("Bibliotecaria") && !conversacionIniciada)
        {
            enRangoBibliotecaria = true;
            if (panelInteraccion != null)
                panelInteraccion.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Ocultar panel cuando se aleja
        if (other.CompareTag("Bibliotecaria"))
        {
            enRangoBibliotecaria = false;
            if (panelInteraccion != null)
                panelInteraccion.SetActive(false);
        }
    }

    void IniciarConversacion()
    {
        conversacionIniciada = true;
        narrativaActiva = true;
        narrativaActual = 0;

        // Ocultar panel de interacción
        if (panelInteraccion != null)
            panelInteraccion.SetActive(false);

        // Activar canvas de narrativa
        if (narrativaCanvas != null)
            narrativaCanvas.SetActive(true);

        // Mostrar y desbloquear cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Detener movimiento
        currentVelocity = Vector3.zero;
        movement = Vector3.zero;

        // Iniciar la secuencia de narrativas
        StartCoroutine(ReproducirNarrativas());
    }

    IEnumerator ReproducirNarrativas()
    {
        // Reproducir cada imagen de narrativa automáticamente
        for (int i = 0; i < narrativas.Length; i++)
        {
            narrativaActual = i;
            
            // Mostrar la imagen actual
            if (imagenNarrativa != null && narrativas[i] != null)
            {
                imagenNarrativa.sprite = narrativas[i];
                imagenNarrativa.enabled = true;
            }

            // Esperar antes de mostrar la siguiente
            yield return new WaitForSeconds(tiempoEntreNarrativas);
        }

        // Cuando terminen todas las narrativas
        TerminarNarrativa();
    }

    void TerminarNarrativa()
    {
        narrativaActiva = false;

        // Desactivar canvas
        if (narrativaCanvas != null)
            narrativaCanvas.SetActive(false);

        // Volver a bloquear cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Reiniciar para futuras conversaciones (opcional)
        // conversacionIniciada = false; // Descomenta si quieres permitir hablar de nuevo
    }

    // Método público para terminar manualmente si lo necesitas
    public void TerminarConversacionManual()
    {
        StopAllCoroutines();
        TerminarNarrativa();
    }
}