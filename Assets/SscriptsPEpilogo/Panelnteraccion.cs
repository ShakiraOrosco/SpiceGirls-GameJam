using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelInteraccion : MonoBehaviour
{
    public Text textoInteraccion;
    public float velocidadParpadeo = 0.5f;

    void OnEnable()
    {
        StartCoroutine(Parpadear());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Parpadear()
    {
        while (true)
        {
            if (textoInteraccion != null)
                textoInteraccion.enabled = !textoInteraccion.enabled;
            
            yield return new WaitForSeconds(velocidadParpadeo);
        }
    }
}