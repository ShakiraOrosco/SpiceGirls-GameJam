using UnityEngine;
using UnityEngine.UI;

public class InteractablePrompt : MonoBehaviour
{
    public GameObject promptUI; // pequeño panel "Presiona E / Interactuar"
    public KeyCode interactKey = KeyCode.E;
    public GameObject narrativeCanvas; // referencia al canvas de slideshow
    private bool playerInRange = false;

    void Start()
    {
        promptUI.SetActive(false);
        narrativeCanvas.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            promptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            promptUI.SetActive(false);
            StartNarrative();
        }
    }

    public void StartNarrative()
    {
        narrativeCanvas.SetActive(true);
        var slideshow = narrativeCanvas.GetComponent<ImageSlideshow>();
        if (slideshow != null) slideshow.Play();
    }

    // Si prefieres usar botón UI para interactuar, crea un UI Button y
    // asigna InteractablePrompt.StartNarrative a su OnClick.
}
