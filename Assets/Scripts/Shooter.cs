using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float fireRate = 8f;         // opcional: disparos por segundo mientras mantienes SPACE

    float nextFireTime;

    void Update()
    {
        bool firePressed = Input.GetKey(KeyCode.Space); // <-- SPACE
        if (firePressed && Time.time >= nextFireTime && firePoint && projectilePrefab)
        {
            // Instancia mirando al mundo (no importa su rotación, ver Projectile abajo)
            Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}
