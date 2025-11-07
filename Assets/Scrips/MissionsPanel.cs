using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionsPanel : MonoBehaviour
{
    [System.Serializable]
    public class MissionUI
    {
        public TMP_Text missionText;    // Texto de la misión
        public Image checkIcon;         // Icono del check
    }

    [Header("Panel de Misiones")]
    public GameObject missionsPanel;

    [Header("Lista de Misiones")]
    public List<MissionUI> missions = new List<MissionUI>();

    private void Start()
    {
        // Asegurar que todos los checks estén desactivados al inicio
        foreach (var mission in missions)
        {
            if (mission.checkIcon != null)
                mission.checkIcon.gameObject.SetActive(false);
        }

        missionsPanel.SetActive(false); // Panel oculto al iniciar
    }

    private void Update()
    {
        // Abrir/cerrar con TAB
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            missionsPanel.SetActive(!missionsPanel.activeSelf);
        }
    }

    // ✅ Llamar para completar una misión
    public void CompletarMision(int index)
    {
        if (index < 0 || index >= missions.Count)
        {
            Debug.LogWarning("Índice de misión inválido: " + index);
            return;
        }

        // Activar el check
        missions[index].checkIcon.gameObject.SetActive(true);

        // Cambiar color del texto como "completado"
        missions[index].missionText.color = Color.green;

        Debug.Log("Misión completada: " + index);
    }
}
