using System.Diagnostics;
using UnityEngine;

public class TecladoMusical : MonoBehaviour
{
    [System.Serializable]
    public class NotaTecla
    {
        public string nombreNota;   // Ejemplo: DO_Grave
        public KeyCode tecla;       // Ejemplo: A, S, D, F, etc.
        public AudioSource sonido;  // Arrastra aquí el AudioSource del botón correspondiente
    }

    public NotaTecla[] notas; // Aquí agregas todas las notas que desees controlar

    void Update()
    {
        foreach (var nota in notas)
        {
            if (Input.GetKeyDown(nota.tecla))
            {
                if (nota.sonido != null)
                {
                    nota.sonido.Play();
                }
                else
                {
                    UnityEngine.Debug.LogWarning("No se asignó un AudioSource para la nota: " + nota.nombreNota);
                }
            }
        }
    }
}
