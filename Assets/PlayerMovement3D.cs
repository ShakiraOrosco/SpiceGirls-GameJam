using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Importante para TextMeshPro

public class PlayerMovement3D : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 2f;
    public float jumpForce = 3f;

    [Header("Opciones")]
    public float modelRotationOffset = 0f;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 movement;
    private bool canInteract = false;
    private bool isGrounded = true;
    private float currentRotation = 0f;

    [Header("UI")]
    public GameObject interactionCanvas;
ñ
    [Header("Narrativa Infiel")]
    public GameObject narrativaCanvas;
    private bool narrativaActiva = false;
    private NPCMovement npcInfiel;

    [Header("Conversación")]
    public GameObject conversacionCanvas;

    [Header("Texto Post-Conversación")]
    public TextMeshProUGUI textoFinal; // Asignar en inspector
    public string nuevoTexto = "¡La conversación ha terminado!"; // Texto que aparecerá

    private string objetoInteractuando; // Para saber con quién interactuamos

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        if (rb != null)
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        if (interactionCanvas != null)
            interactionCanvas.SetActive(false);

        if (narrativaCanvas != null)
            narrativaCanvas.SetActive(false);

        if (conversacionCanvas != null)
            conversacionCanvas.SetActive(false);
    }

    void Update()
    {
        // Movimiento sigue activo siempre
        float rotationInput = Input.GetAxis("Horizontal");
        currentRotation = Mathf.Lerp(currentRotation, rotationInput, rotationSpeed * Time.deltaTime);
        float rotationAmount = currentRotation * 50f * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0f, rotationAmount, 0f);

        if (Mathf.Abs(modelRotationOffset) > 0.001f)
            transform.rotation = Quaternion.Euler(0f, modelRotationOffset, 0f) * transform.rotation;

        float moveZ = Input.GetAxis("Vertical");
        movement = (Quaternion.Euler(0f, modelRotationOffset, 0f) * transform.forward) * moveZ;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (animator != null)
            animator.SetBool("isWalking", Mathf.Abs(moveZ) > 0.1f);

        // Interacción solo si narrativaActiva es false
        if (!narrativaActiva && canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (objetoInteractuando == "Infiel")
            {
                narrativaCanvas.SetActive(true);
                narrativaActiva = true;
                if (interactionCanvas != null)
                    interactionCanvas.SetActive(false);
                if (conversacionCanvas != null)
                    conversacionCanvas.SetActive(true);
            }
            else if (objetoInteractuando == "Gato")
            {
                SceneManager.LoadScene("NarrativaInicio");
            }
        }
    }


    void FixedUpdate()
    {

        if (rb != null)
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gato") || other.CompareTag("Infiel"))
        {
            canInteract = true;
            objetoInteractuando = other.tag;

            if (interactionCanvas != null)
                interactionCanvas.SetActive(true);

            if (other.CompareTag("Infiel"))
                npcInfiel = other.GetComponent<NPCMovement>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gato") || other.CompareTag("Infiel"))
        {
            canInteract = false;
            objetoInteractuando = null;
            npcInfiel = null;
            if (interactionCanvas != null)
                interactionCanvas.SetActive(false);
        }
    }

    public void TerminarNarrativa()
    {
        if (narrativaCanvas != null)
            narrativaCanvas.SetActive(false);

        if (conversacionCanvas != null)
            conversacionCanvas.SetActive(false);

        narrativaActiva = false;

        if (npcInfiel != null)
            npcInfiel.enabled = true;

        // Cambiar texto al terminar la conversación
        if (textoFinal != null)
            textoFinal.text = nuevoTexto;
    }
}