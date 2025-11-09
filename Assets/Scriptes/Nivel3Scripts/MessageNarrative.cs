using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class MessageNarrative : MonoBehaviour
{
    public Text messageText;
    public List<string> messages = new List<string>();
    public float secondsPerMessage = 3f;
    public UnityEvent onNarrativeEnd;

    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    IEnumerator PlayRoutine()
    {
        for (int i = 0; i < messages.Count; i++)
        {
            messageText.text = messages[i];
            yield return new WaitForSeconds(secondsPerMessage);
        }

        onNarrativeEnd?.Invoke();
    }
}
