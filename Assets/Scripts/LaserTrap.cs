using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public int damagePerSecond = 10;
    private bool playerInside = false;
    private PlayerHealth targetPlayerHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetPlayerHealth = other.GetComponent<PlayerHealth>();
            playerInside = true;
            Debug.Log("⚠️ ¡Atención! Cruzaste la línea del rayo láser.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            targetPlayerHealth = null;
            Debug.Log("✅ Saliste de la zona del rayo láser.");
        }
    }

    void Update()
    {
        // Si el jugador se queda cruzando el láser, descuenta vida continuamente
        if (playerInside && targetPlayerHealth != null)
        {
            targetPlayerHealth.TakeDamage(Mathf.RoundToInt(damagePerSecond * Time.deltaTime));
        }
    }
}