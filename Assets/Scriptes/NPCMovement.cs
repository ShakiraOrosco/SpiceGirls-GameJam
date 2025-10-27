using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [Header("Configuración de movimiento")]
    public Transform[] puntosDestino;  // Lista de puntos
    public float velocidad = 3f;       // Velocidad del NPC
    public float distanciaMinima = 0.2f;
    public bool ciclo = true;          // Si vuelve al primer punto al terminar

    [Header("Animación")]
    public string boolCaminar = "isWalking";

    private int indiceActual = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        if (puntosDestino.Length == 0)
        {
            UnityEngine.Debug.LogWarning("⚠️ No se asignaron puntos de destino al NPC.");
            enabled = false;
        }

        // 🔹 Comienza desactivado hasta que el jugador termine la narrativa
        if (enabled && puntosDestino.Length > 0)
            animator.SetBool(boolCaminar, false);
    }

    void Update()
    {
        if (puntosDestino.Length == 0) return;

        Transform destinoActual = puntosDestino[indiceActual];

        Vector3 direccion = (destinoActual.position - transform.position);
        direccion.y = 0;

        float distancia = direccion.magnitude;

        if (distancia > distanciaMinima)
        {
            if (animator != null)
                animator.SetBool(boolCaminar, true);

            Quaternion rotacion = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 5f);

            transform.position += transform.forward * velocidad * Time.deltaTime;
        }
        else
        {
            if (animator != null)
                animator.SetBool(boolCaminar, false);

            indiceActual++;
            if (indiceActual >= puntosDestino.Length)
            {
                if (ciclo)
                    indiceActual = 0;
                else
                    enabled = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < puntosDestino.Length - 1; i++)
        {
            if (puntosDestino[i] && puntosDestino[i + 1])
                Gizmos.DrawLine(puntosDestino[i].position, puntosDestino[i + 1].position);
        }
    }
}
