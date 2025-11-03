using UnityEngine;
using UnityEngine.UI;

public class CanvasDecision : MonoBehaviour
{
    [Header("Configuración de tiempos")]
    public float tiempoEspera = 10f; // segundos antes de mostrar el canvas

    [Header("Referencias de Canvas")]
    public GameObject canvasDecision; // Canvas con botones Sí / No
    public GameObject canvasNarrativa; // Canvas que muestra la narrativa

    [Header("Objetos a eliminar")]
    public GameObject objetoSi; // Objeto que se destruye si elige "Sí"
    public GameObject objetoNo; // Objeto que se destruye si elige "No"

    [Header("Botones de decisión")]
    public Button botonSi;
    public Button botonNo;

    private void Start()
    {
        // Desactivar los canvas al inicio
        if (canvasDecision != null) canvasDecision.SetActive(false);
        if (canvasNarrativa != null) canvasNarrativa.SetActive(false);

        // Esperar 10 segundos para mostrar el canvas
        Invoke(nameof(MostrarCanvasDecision), tiempoEspera);

        // Asignar eventos a los botones
        if (botonSi != null) botonSi.onClick.AddListener(OpcionSi);
        if (botonNo != null) botonNo.onClick.AddListener(OpcionNo);
    }

    void MostrarCanvasDecision()
    {
        if (canvasDecision != null)
            canvasDecision.SetActive(true);
    }

    void OpcionSi()
    {
        // Mostrar narrativa y destruir el objeto asignado
        if (canvasNarrativa != null) canvasNarrativa.SetActive(true);
        if (canvasDecision != null) canvasDecision.SetActive(false);
        if (objetoSi != null) Destroy(objetoSi);
    }

    void OpcionNo()
    {
        // Desactivar canvas y eliminar el otro objeto
        if (canvasDecision != null) canvasDecision.SetActive(false);
        if (objetoNo != null) Destroy(objetoNo);
    }
}
