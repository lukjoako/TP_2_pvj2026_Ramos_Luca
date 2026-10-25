using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int scoreValue = 10; // Puntos que otorga el ítem

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el que entra en contacto es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Disco de datos recolectado! Puntos sumados: " + scoreValue);
            
            // Aquí puedes sumar los puntos a un GameManager o contador global más adelante

            // Destruye el objeto de la escena para que desaparezca
            Destroy(gameObject);
        }
    }
}