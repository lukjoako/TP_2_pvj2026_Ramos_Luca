using UnityEngine;

public class ConsolaInteractiva : MonoBehaviour, IInteractable
{
    public string mensajeTerminal = "¡Terminal de datos hackeada con éxito!";

    public void Interact()
    {
        Debug.Log(mensajeTerminal);
        // Aquí más adelante sumaremos puntos o abriremos puertas conectando con el GameManager
    }
}