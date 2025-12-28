using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public Transform player;
    public Transform turretHead; // This should be your Sphere
    public GameObject laserPrefab;
    public Transform firePoint; // At the tip of the barrel
    public float fireRate = 2f;
    private float nextFireTime;

    void Update()
    {
        if (player == null || turretHead == null) return;

        // This forces the sphere to point exactly at the player camera
       // This flips the rotation so the back of the sphere faces the player
        turretHead.LookAt(2 * turretHead.position - player.position);

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        if (laserPrefab != null && firePoint != null)
        {
            // Spawn the laser
            GameObject bullet = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            
            // PHYSICS FIX: Ensure gravity is OFF on the spawned laser immediately
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.velocity = Vector3.zero; // Stop it from "falling" on spawn
            }
        }
    }
}