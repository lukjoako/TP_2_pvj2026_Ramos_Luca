using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento y Salto")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;

    [Header("Chequeo de Suelo (Raycast)")]
    public Transform groundCheck; // Un objeto vacío ubicado en los pies del jugador
    public float raycastDistance = 0.2f;
    public LayerMask groundLayer; // Seleccionar la capa "Ground" en el Inspector
    private bool isGrounded;

    [Header("Interacción a Distancia (Raycast Frontal)")]
    public Camera playerCamera; // La cámara principal o la del jugador
    public float interactionDistance = 3f;
    public LayerMask interactableLayer; // Seleccionar la capa "Interactable" en el Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        // 1. CHEQUEO DE SUELO CON RAYCAST
        // Proyecta un rayo hacia abajo desde el transform "groundCheck"
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, raycastDistance, groundLayer);

        // Salto condicionado al chequeo de suelo
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // 2. INTERACCIÓN A DISTANCIA CON RAYCAST Y LAYERMASK
        if (Input.GetKeyDown(KeyCode.E)) // Tecla E para interactuar
        {
            DetectAndInteract();
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // 1. Obtenemos la dirección de la cámara principal (ignorando la altura Y para que sea plano)
        Vector3 camForward = playerCamera.transform.forward;
        Vector3 camRight = playerCamera.transform.right;
        
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // 2. Calculamos el movimiento relativo a hacia dónde apunta la cámara
        Vector3 moveDir = (camForward * moveZ + camRight * moveX).normalized;

        // 3. Si nos estamos moviendo, hacemos que el jugador rote suavemente hacia esa dirección
        if (moveDir.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 0.2f));
        }

        // 4. Aplicamos la velocidad manteniendo la física de gravedad en Y
        Vector3 targetVelocity = moveDir * moveSpeed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }

    void DetectAndInteract()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Lanzamos el rayo frontal filtrando únicamente con la LayerMask de objetos interactivos
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            // Verificamos si el objeto impactado tiene el script de interacción
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    // Visualizar los rayos en la pestaña Scene para depurar fácilmente
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * raycastDistance);
        }

        if (playerCamera != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionDistance);
        }
    }
}