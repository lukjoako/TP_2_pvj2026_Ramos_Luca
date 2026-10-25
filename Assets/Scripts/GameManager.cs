using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Patrón Singleton para acceder fácil al GameManager desde cualquier otro script
    public static GameManager Instance;

    public enum GameState { Start, Playing, Paused, Win, Lose }
    public GameState currentState;

    [Header("Estadísticas de Partida")]
    public int score = 0;
    public int playerHealth = 100;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ChangeState(GameState.Playing);
    }

    void Update()
    {
        // Presionar 'P' o 'Escape' para pausar o reanudar el juego
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                ChangeState(GameState.Paused);
            }
            else if (currentState == GameState.Paused)
            {
                ChangeState(GameState.Playing);
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log("🎮 [ESTADO DEL JUEGO]: " + currentState);

        switch (currentState)
        {
            case GameState.Playing:
                Time.timeScale = 1f; // Reanuda el tiempo
                break;

            case GameState.Paused:
                Time.timeScale = 0f; // Congela el tiempo por completo
                Debug.Log("⏸️ Juego Pausado. Tiempo congelado.");
                break;

            case GameState.Win:
                Time.timeScale = 0f;
                Debug.Log("🏆 ¡VICTORIA! Puntaje final: " + score);
                break;

            case GameState.Lose:
                Time.timeScale = 0f;
                Debug.Log("💀 ¡DERROTA! El jugador fue neutralizado.");
                break;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("⭐ Puntaje actual: " + score);
    }
}