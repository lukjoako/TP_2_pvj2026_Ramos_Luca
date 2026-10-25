using UnityEngine;

public class MudZone : MonoBehaviour
{
    public float slowFactor = 0.5f; // Reduce la velocidad a la mitad

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Asumiendo que tu script de movimiento se llama PlayerController
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.moveSpeed *= slowFactor;
                Debug.Log("⚠️ Entraste al barro: velocidad reducida.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.moveSpeed /= slowFactor; // Restablece la velocidad original
                Debug.Log("✅ Saliste del barro: velocidad normal.");
            }
        }
    }
}