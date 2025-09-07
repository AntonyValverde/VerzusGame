using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ScrollBackground : MonoBehaviour
{
    public float scrollX = 0.03f;  // + a la derecha, - a la izquierda
    public float scrollY = 0f;     // + hacia arriba, - abajo

    Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        // Instancia el material para no modificar el asset original del Project
        rend.material = new Material(rend.material);
    }

    void Update()
    {
        var o = rend.material.mainTextureOffset;
        o.x += scrollX * Time.deltaTime;
        o.y += scrollY * Time.deltaTime;
        rend.material.mainTextureOffset = o;
    }
}
