using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Salud del Jugador")]
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Salud inicial del jugador: " + currentHealth);
    }

    // Método para recibir daño
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("¡Te han atacado! Vida actual: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¡Game Over! El jugador ha sido neutralizado.");
        // Aquí puedes reiniciar la escena o mandar al jugador al punto de spawn
        // Ej: UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}