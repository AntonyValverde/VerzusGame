using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ParallaxBackground : MonoBehaviour
{
    public Transform player;

    public enum TextureAxis { X, Y }
    [Header("Eje de la textura a desplazar")]
    public TextureAxis axis = TextureAxis.X; // Por defecto movemos el offset en X

    [Header("Cuánto desplazar por unidad recorrida")]
    public float factorZ = 0.2f; // +Z del jugador => avance del fondo
    public float factorX = 0.2f; // +X del jugador => retroceso del fondo

    [Header("Invertir direcciones")]
    public bool invertZ = false; // si quieres que +Z vaya al revés, marca esto
    public bool invertX = true;  // por defecto, +X hace 'fondo hacia atrás'

    [Header("Umbral para considerarlo quieto")]
    public float stillThreshold = 0.0001f;

    Renderer rend;
    Vector3 lastPlayerPos;
    bool hasLast;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        // Clonar material para no modificar el asset del Project
        rend.material = new Material(rend.material);

        if (!player)
        {
            var p = GameObject.Find("Player");
            if (p) player = p.transform;
        }
    }

    void LateUpdate()
    {
        if (!player) return;

        if (!hasLast)
        {
            lastPlayerPos = player.position;
            hasLast = true;
            return;
        }

        Vector3 delta = player.position - lastPlayerPos;
        lastPlayerPos = player.position;

        // Si no se mueve, no hay scroll
        if (delta.sqrMagnitude <= stillThreshold) return;

        float dz = delta.z * (invertZ ? -1f : 1f);
        float dx = delta.x * (invertX ? -1f : 1f);

        // Regla: moverse +Z => "avanzar"; moverse +X => "retroceder"
        float deltaOffset = dz * factorZ + dx * factorX;

        Vector2 o = rend.material.mainTextureOffset;
        if (axis == TextureAxis.X) o.x += deltaOffset;
        else o.y += deltaOffset;

        rend.material.mainTextureOffset = o;
    }
}
