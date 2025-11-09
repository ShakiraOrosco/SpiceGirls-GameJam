using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject panelPausa; // Asigna el Panel desde el inspector
    private bool juegoPausado = false;

    void Update()
    {
        // Detectar ESC para pausar/despausar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausa();
        }
    }

    public void TogglePausa()
    {
        juegoPausado = !juegoPausado;

        if (juegoPausado)
        {
            ActivarPausa();
        }
        else
        {
            ReanudarJuego();
        }
    }

    void ActivarPausa()
    {
        panelPausa.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego
        
        // ✅ Mostrar el cursor y permitir control solo con teclas
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReanudarJuego()
    {
        panelPausa.SetActive(false);
        Time.timeScale = 1f; // Reanuda el juego
        juegoPausado = false;
        
        // ✅ Mantener el cursor visible (sin bloquearlo)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        // ✅ Cursor visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        // ✅ Cursor visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu"); // Cambia por el nombre de tu escena de menú
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
