using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ProjectileM4A1 : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 60f;     
    public float maxDistance = 60f;   
    public float lifeTime = 3f;        

    [Header("Daño (opcional)")]
    public float damage = 10f;      

    Rigidbody rb;                     
    Vector3 startPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }

    void OnEnable()
    {
        startPos = transform.position;

        // limpiar velocidad acumulada
        if (rb) rb.velocity = Vector3.zero;

        // respaldo por tiempo
        CancelInvoke();
        Invoke(nameof(Die), lifeTime);
    }

    void FixedUpdate()
    {
        Vector3 step = transform.forward * speed * Time.fixedDeltaTime;

        if (rb) rb.MovePosition(rb.position + step);
        else transform.position += step;
        if ((transform.position - startPos).sqrMagnitude >= maxDistance * maxDistance)
            Die();
    }

    void OnCollisionEnter(Collision hit)
    {
        // Ejemplo de aplicación de daño (si tu objetivo tiene un componente con TakeDamage)
        // var dmg = hit.collider.GetComponent<ITakeDamage>();
        // if (dmg != null) dmg.TakeDamage(damage);

        Die();
    }

    void Die()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
}
