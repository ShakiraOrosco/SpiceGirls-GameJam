
using UnityEngine;
using UnityEngine.UI;

public class ControlB : MonoBehaviour
{
    public Button[] botones; // Asigna tus 14 botones en el inspector
    private int indiceActual = 0;

    void Start()
    {
        // Al iniciar, ocultamos todos los botones menos el primero
        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].gameObject.SetActive(i == 0);
        }
    }

    void Update()
    {
        // Si se presiona la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (indiceActual < botones.Length)
            {
                // Simular clic en el botón actual
                botones[indiceActual].onClick.Invoke();

                // Ocultar el botón actual
                botones[indiceActual].gameObject.SetActive(false);

                // Avanzar al siguiente botón si existe
                indiceActual++;

                if (indiceActual < botones.Length)
                {
                    botones[indiceActual].gameObject.SetActive(true);
                }
            }
        }
    }
}
