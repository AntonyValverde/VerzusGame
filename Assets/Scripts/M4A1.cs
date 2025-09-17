using UnityEngine;

public class M4A1 : MonoBehaviour
{
    [Header("Referencias")]
    public Transform muzzle;           
    public GameObject bulletPrefab;
    public ParticleSystem muzzleFlash;
    public AudioSource audioSource; 
    public AudioClip shootSound;

    [Header("Stats")]
    public float fireRate = 8f;

    float nextFireTime;

    public bool CanFire() => Time.time >= nextFireTime;

    public void Fire()
    {
        if (!CanFire() || !muzzle || !bulletPrefab) return;
        Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        if (muzzleFlash) muzzleFlash.Play();
        if (audioSource && shootSound) audioSource.PlayOneShot(shootSound);
        nextFireTime = Time.time + 1f / fireRate;
    }
}
