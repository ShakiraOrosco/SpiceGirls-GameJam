using UnityEngine;
using UnityEngine.UI;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject notificationCanvas;   // panel con imagen + boton Contestar
    public AudioSource audioSource;         // para reproducir notificación
    public AudioClip defaultNotificationSound;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void FriendReachedEnd(Vector3 pos, AudioClip clip = null)
    {
        // reproducir audio
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip != null ? clip : defaultNotificationSound);
        }

        // activar canvas de notificación (muestra la imagen y boton contestar)
        if (notificationCanvas != null) notificationCanvas.SetActive(true);
    }
}
