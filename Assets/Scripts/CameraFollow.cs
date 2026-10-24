using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El objeto del jugador
    public float smoothSpeed = 0.125f; // Velocidad de seguimiento suave
    public Vector3 offset = new Vector3(0f, 3f, -5f); // X: lateral, Y: altura, Z: distancia detrás

    void LateUpdate()
    {
        if (target == null) return;

        // Creamos una rotación que solo toma en cuenta el giro horizontal (eje Y) del jugador,
        // evitando que cualquier inclinación extraña mueva la cámara hacia abajo.
        Quaternion yawRotation = Quaternion.Euler(0f, target.eulerAngles.y, 0f);

        // Calculamos la posición deseada aplicando esa rotación limpia al offset
        Vector3 desiredPosition = target.position + yawRotation * offset;
        
        // Movimiento suave (Lerp) hacia la posición deseada
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Hace que la cámara siempre apunte al jugador (con un leve ajuste vertical en Y)
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}