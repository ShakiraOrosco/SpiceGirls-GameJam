using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Botones del Menú")]
    public Button botonIniciar;
    public Button botonSobreApp;
    public Button botonSalir;

    [Header("Nombre de escenas")]
    public string nombreEscenaJuego = "Nivel1"; //Cambiar luego a epilogo
    public string nombreEscenaSobreApp = "SobreApp";

    private void Start()
    {
          // ✅ Asegurar que el cursor sea visible al abrir el menú
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

        botonIniciar.onClick.AddListener(IniciarJuego);
        botonSobreApp.onClick.AddListener(IrASobreApp);
        botonSalir.onClick.AddListener(SalirDelJuego);
    }

    public void IniciarJuego()
    {
        SceneManager.LoadScene(nombreEscenaJuego);
    }

    public void IrASobreApp()
    {        
        SceneManager.LoadScene("SobreApp");
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
