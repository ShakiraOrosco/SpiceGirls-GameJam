using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerCelular : MonoBehaviour
{
    // Tag del objeto que activará la narrativa
    public string tagObjeto = "Celular";

    // Nombre de la escena a cargar
    public string escenaNarrativa = "NarrativaInfidelidad";

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra al trigger es el Player
        if (other.CompareTag("Player"))
        {
            // Busca si el Player está interactuando con el objeto correcto
            if (CompareTag(tagObjeto))
            {
                // Cargar la escena de narrativa
                SceneManager.LoadScene(escenaNarrativa);
            }
        }
    }
}
