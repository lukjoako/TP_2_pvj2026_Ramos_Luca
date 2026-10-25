using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    [Header("Configuración de la Puerta")]
    public int requiredScore = 10; // Puntuación necesaria para abrirla
    public float openDistance = 3f; // Distancia o desplazamiento al abrirse (ej. sube o se mueve)
    private bool isOpen = false;

    // Este método se ejecuta cuando el jugador presiona 'E' y el raycast la detecta
    public void Interact()
    {
        if (isOpen) return; // Si ya está abierta, no hace nada

        // Verificamos si el GameManager existe y si el puntaje es suficiente
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.score >= requiredScore)
            {
                OpenDoor();
            }
            else
            {
                Debug.Log("🔒 Puerta bloqueada. Necesitas al menos " + requiredScore + " puntos. Tienes: " + GameManager.Instance.score);
            }
        }
        else
        {
            Debug.LogWarning("⚠️ No se encontró el GameManager en la escena.");
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        Debug.Log("🔓 ¡Acceso concedido! Abriendo puerta...");

        // Opción 1: Mover la puerta hacia arriba para abrirla
        transform.position += new Vector3(0f, openDistance, 0f);

        // (Opcional) Si prefieres que desaparezca por completo, descomenta la siguiente línea:
        // Destroy(gameObject);
    }
}