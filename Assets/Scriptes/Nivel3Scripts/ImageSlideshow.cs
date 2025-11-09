using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;



public class ImageSlideshow : MonoBehaviour
{
    public Image displayImage;            // componente UI Image donde se muestran sprites
    public List<Sprite> slides = new List<Sprite>();
    public float secondsPerSlide = 5f;
    public bool loop = false;
    public UnityEvent onSlideshowEnd;     // asignar en inspector: iniciar FriendController

    private Coroutine playCoroutine;

    public void Play()
    {
        if (playCoroutine != null) StopCoroutine(playCoroutine);
        playCoroutine = StartCoroutine(PlayRoutine());
    }

    IEnumerator PlayRoutine()
    {
        for (int i = 0; i < slides.Count; i++)
        {
            displayImage.sprite = slides[i];
            yield return new WaitForSeconds(secondsPerSlide);
        }

        displayImage.sprite = null;

        if (loop)
            Play();
        else
        {
            onSlideshowEnd?.Invoke();
            gameObject.SetActive(false); // 🔹 Desactiva el canvas
        }
    }


    public void StopPlay()
    {
        if (playCoroutine != null) StopCoroutine(playCoroutine);
    }
}
