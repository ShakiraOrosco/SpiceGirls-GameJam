using UnityEngine;
using UnityEngine.UI;

public class NotificationUI : MonoBehaviour
{
    public Button contestarButton;
    public GameObject messageNarrativeCanvas; // canvas que mostrará narración de mensajes
    public Image messageImage; // si quieres mostrar imagen en el notificationCanvas
    public Sprite friendMessageSprite; // imagen a mostrar en notification

    void Start()
    {
        if (contestarButton != null) contestarButton.onClick.AddListener(OnContestar);
        if (messageImage != null && friendMessageSprite != null) messageImage.sprite = friendMessageSprite;
    }

    void OnContestar()
    {
        gameObject.SetActive(false); // ocultar notification
        if (messageNarrativeCanvas != null)
        {
            messageNarrativeCanvas.SetActive(true);
            var slideshow = messageNarrativeCanvas.GetComponent<ImageSlideshow>();
            if (slideshow != null) slideshow.Play();
            else
            {
                // si es narrativa de texto, invoca método para mostrar
                var msgNarr = messageNarrativeCanvas.GetComponent<MessageNarrative>();
                if (msgNarr != null) msgNarr.Play();
            }
        }
    }
}
