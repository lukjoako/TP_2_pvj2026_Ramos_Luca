using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player; // Arrastra al jugador aquí en el Inspector

    [Header("Patrulla")]
    public Transform[] patrolPoints; // Puntos por donde patrullará el dron
    private int currentPointIndex = 0;

    [Header("Detección y Persecución")]
    public float visionRange = 10f; // Distancia a la que detecta al jugador
    private enum State { Patrolling, Chasing };
    private State currentState = State.Patrolling;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Si no se asignó el jugador manualmente, lo busca por la etiqueta "Player"
        if (player == null)
        {
            GameObject pObj = GameObject.FindGameObjectWithTag("Player");
            if (pObj != null) player = pObj.transform;
        }

        GoToNextPatrolPoint();
    }

   void Update()
    {
        if (player == null || agent == null) return;

        // Validamos que el agente esté activo y sobre la malla antes de hacer cálculos
        if (!agent.enabled || !agent.isOnNavMesh) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrolling:
                // Si el jugador entra en el radio de visión, cambia a persecución
                if (distanceToPlayer <= visionRange)
                {
                    Debug.Log("¡Jugador detectado! Iniciando persecución...");
                    currentState = State.Chasing;
                }
                // Si llegó al punto de patrulla actual, pasa al siguiente
                else if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    GoToNextPatrolPoint();
                }
                break;

            case State.Chasing:
                // El agente persigue la posición del jugador en tiempo real
                agent.SetDestination(player.position);

                // Si el jugador escapa y se aleja demasiado, vuelve a patrullar
                if (distanceToPlayer > visionRange * 1.5f)
                {
                    Debug.Log("El jugador escapó. Volviendo a patrulla...");
                    currentState = State.Patrolling;
                    GoToNextPatrolPoint();
                }
                break;
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        agent.destination = patrolPoints[currentPointIndex].position;
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
    }

    // Dibuja el radio de visión en amarillo en la ventana Scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}