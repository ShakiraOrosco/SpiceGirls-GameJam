using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeresaMovimiento : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 3f;
    public float acceleration = 1f;
    public float deceleration = 5f;
    public float jumpForce = 3f;

    [Header("Rotación con Teclas")]
    public float rotationSpeed = 100f; // Velocidad de rotación con A y D

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
    public float tiempoEntreNarrativas = 7f; // Tiempo entre cada narrativa

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

        // Rotación con A y D
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotationInput * rotationSpeed * Time.deltaTime);

        // Movimiento adelante/atrás con W y S
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = (transform.forward * moveZ).normalized;

        // Suavizado del movimiento
        if (moveDirection.magnitude > 0.1f)
            currentVelocity = Vector3.Lerp(currentVelocity, moveDirection * moveSpeed, acceleration * Time.deltaTime);
        else
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);

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
            IniciarConversacion();
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
                puerta.AbrirPuerta();
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

            if (imagenNarrativa != null && narrativas[i] != null)
            {
                imagenNarrativa.sprite = narrativas[i];
                imagenNarrativa.enabled = true;
            }

            yield return new WaitForSeconds(tiempoEntreNarrativas);
        }

        TerminarNarrativa();
    }

    void TerminarNarrativa()
    {
        narrativaActiva = false;

        // Desactivar canvas
        if (narrativaCanvas != null)
            narrativaCanvas.SetActive(false);
    }

    public void TerminarConversacionManual()
    {
        StopAllCoroutines();
        TerminarNarrativa();
    }
}
