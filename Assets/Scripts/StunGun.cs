using UnityEngine;
using System.Collections;

public class StunGun : MonoBehaviour
{
    public Camera playerCamera;
    public float range = 15f;
    public LayerMask enemyLayer; // Capa para detectar al enemigo
    public float stunDuration = 3f; // Tiempo que estará paralizado

    void Update()
    {
        // Presiona la tecla 'F' o el Botón Izquierdo del Mouse para disparar el aturdidor
        if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
        {
            ShootStunRay();
        }
    }

    void ShootStunRay()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, enemyLayer))
        {
            Debug.Log("⚡ ¡Rayo aturdidor impactó a: " + hit.collider.name + "!");
            
            // Intenta obtener el script de IA del enemigo
            EnemyAI enemy = hit.collider.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                StartCoroutine(StunRoutine(enemy));
            }
        }
    }

    IEnumerator StunRoutine(EnemyAI enemy)
    {
        // Desactiva temporalmente el script de IA o frena su agente
        UnityEngine.AI.NavMeshAgent agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.isStopped = true;
        }
        
        enemy.enabled = false;
        Debug.Log("🛑 ¡Enemigo paralizado!");

        yield return new WaitForSeconds(stunDuration);

        // Reactiva al enemigo pasado el tiempo
        if (agent != null)
        {
            agent.isStopped = false;
        }
        enemy.enabled = true;
        Debug.Log("▶️ El enemigo recuperó el movimiento.");
    }
}