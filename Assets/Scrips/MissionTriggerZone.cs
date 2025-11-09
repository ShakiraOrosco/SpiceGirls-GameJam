using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTriggerZone : MonoBehaviour
{
    [Header("Número de misión a completar")]
    public int missionIndex; // 0 = misión 1, 1 = misión 2...

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Completa la misión
            FindObjectOfType<MissionsPanel>().CompletarMision(missionIndex);

            // Destruye el trigger para que no se repita
            Destroy(gameObject);
        }
    }
}
