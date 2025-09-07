using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                 // Player
    public Vector3 offset = new Vector3(0, 4, -6);
    public float smooth = 8f;

    void LateUpdate()
    {
        if (!target) return;

        // OFFSET EN ESPACIO MUNDIAL (no gira con el jugador)
        // X = separación lateral, Y = altura, Z = desplazamiento delante/detrás
        Vector3 desired = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, desired, smooth * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.2f);
    }

}
