using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class ReproductorImagenes : MonoBehaviour
{
    [Header("Imágenes a reproducir")]
    public Sprite[] imagenes;                     // Lista de imágenes que se mostrarán
    public UnityEngine.UI.Image displayImage;     // Objeto UI donde se mostrará la imagen

    [Header("Configuración")]
    public float tiempoEntreImagenes = 2f;        // Tiempo entre cada imagen (segundos)
    public string nombreEscenaSiguiente;          // Nombre de la escena a cargar al finalizar

    private int indiceActual = 0;

    void Start()
    {
        if (imagenes.Length > 0 && displayImage != null)
        {
            displayImage.sprite = imagenes[0];
            indiceActual = 0;
            StartCoroutine(ReproducirImagenes());
        }
        else
        {
            UnityEngine.Debug.LogWarning("⚠️ No hay imágenes o no se asignó displayImage en el inspector.");
        }
    }

    private IEnumerator ReproducirImagenes()
    {
        while (indiceActual < imagenes.Length)
        {
            displayImage.sprite = imagenes[indiceActual];
            indiceActual++;
            yield return new WaitForSeconds(tiempoEntreImagenes);
        }

        CambiarEscena();
    }

    private void CambiarEscena()
    {
        if (!string.IsNullOrEmpty(nombreEscenaSiguiente))
        {
            SceneManager.LoadScene(nombreEscenaSiguiente);
        }
        else
        {
            UnityEngine.Debug.LogWarning("⚠️ No se ha especificado el nombre de la siguiente escena.");
        }
    }
}
