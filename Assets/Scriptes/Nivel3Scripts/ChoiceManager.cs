using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public Button stayButton;
    public Button noStayButton;
    public GameObject loseCanvas;
    public GameObject finalNarrativeCanvas;

    void Start()
    {
        stayButton.onClick.AddListener(OnStay);
        noStayButton.onClick.AddListener(OnNoStay);
    }

    void OnStay()
    {
        // mostrar pantalla de perder
        if (loseCanvas != null) loseCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    void OnNoStay()
    {
        if (finalNarrativeCanvas != null) finalNarrativeCanvas.SetActive(true);
        gameObject.SetActive(false);
    }


    public void ActivateObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(false);
    }

}
