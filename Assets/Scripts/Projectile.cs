using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;        // unidades por segundo
    public float maxDistance = 30f;  // “tantos espacios” a recorrer en Z
    public float lifeTime = 5f;      // respaldo por si no choca (opcional)

    Rigidbody rb;
    Vector3 startPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        startPos = transform.position;
        CancelInvoke();
        Invoke(nameof(Die), lifeTime); // backup
    }

    void FixedUpdate()
    {
        // Avanza en Z del mundo (Vector3.forward)
        Vector3 next = rb ? rb.position + Vector3.forward * speed * Time.fixedDeltaTime
                          : transform.position + Vector3.forward * speed * Time.fixedDeltaTime;

        if (rb) rb.MovePosition(next);
        else transform.position = next;

        if (Mathf.Abs(transform.position.z - startPos.z) >= maxDistance)
            Die();
    }

    void OnCollisionEnter(Collision _) => Die();

    void Die()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
}
