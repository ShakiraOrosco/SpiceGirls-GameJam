using UnityEngine;
 
public class ZonaVacio : MonoBehaviour

{

    private void OnTriggerEnter(Collider other)

    {

        if (other.CompareTag("Player"))

        {

            Debug.Log("Jugador cayó al vacío");

            // Por ejemplo:

            other.transform.position = new Vector3(12.193f, 0.045f, -3.964f); // Punto de respawn

        }

    }

}

 