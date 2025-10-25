using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement3D : MonoBehaviour
{
    public float moveSpeed = 12f;          // 🔹 Velocidad aumentada de 8f a 12f
    public float rotationSpeed = 5f;       // 🔹 Velocidad de rotación
    public float jumpForce = 5f;           // 🔹 Fuerza del salto
    
    private Rigidbody rb;                  // Referencia al Rigidbody
    private Vector3 movement;              // Dirección de movimiento
    private bool canInteract = false;      // Si está cerca del gato
    private bool isGrounded = true;        // 🔹 Si está en el suelo
    
    private float currentRotation = 0f;    // 🔹 Rotación actual suavizada
    
    [Header("UI")]
    public GameObject interactionCanvas;   // Canvas con el mensaje
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("⚠️ El objeto no tiene un componente Rigidbody.");
        }
        else
        {
            // Congelar rotación en X y Z para evitar que se voltee
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        
        if (interactionCanvas != null)
        {
            interactionCanvas.SetActive(false); // Ocultar el Canvas al inicio
        }
    }
    
    void Update()
    {
        // Leer input de rotación (A/D o flechas laterales)
        float rotationInput = Input.GetAxis("Horizontal");
        
        // Suavizar la rotación usando Lerp
        currentRotation = Mathf.Lerp(currentRotation, rotationInput, rotationSpeed * Time.deltaTime);
        
        // Aplicar rotación suave
        transform.Rotate(0f, currentRotation * 50f * Time.deltaTime, 0f);
        
        // Leer input de movimiento (W/S o flechas verticales)
        float moveZ = Input.GetAxis("Vertical");
        
        // Calcular movimiento hacia adelante o atrás
        movement = transform.forward * moveZ;
        
        // 🔹 SALTO con Space
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        
        // Si el jugador puede interactuar y presiona E
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("NarrativaInicio");
        }
    }
    
    void FixedUpdate()
    {
        // Aplicar movimiento más rápido
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // 🔹 Detectar si toca el suelo
        isGrounded = true;
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Si el jugador entra en contacto con el Gato
        if (other.CompareTag("Gato"))
        {
            canInteract = true;
            if (interactionCanvas != null)
                interactionCanvas.SetActive(true);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        // Si se aleja del Gato
        if (other.CompareTag("Gato"))
        {
            canInteract = false;
            if (interactionCanvas != null)
                interactionCanvas.SetActive(false);
        }
    }
}