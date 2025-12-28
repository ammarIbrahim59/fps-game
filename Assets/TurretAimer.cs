using UnityEngine;

public class TurretAimer : MonoBehaviour
{
    [Header("Setup References")]
    public Transform player;           
    public Transform turretBase;       
    public Transform turretHead;       
    public GameObject laserPrefab;     
    public Transform firePoint;        

    [Header("Stats")]
    public float range = 20f;
    public float rotationSpeed = 5f;
    public float fireRate = 1f;
    public float playerCenterOffset = 1.5f; 

    [Header("Model Alignment")]
    public float headRotationOffset = -90f; 

    [Header("Health System")]
    public int totalHealth = 3;
    public GameObject explosionEffect;

    private float fireCountdown = 0f;

    public void TakeDamage(int damage)
    {
        totalHealth -= damage;
        Debug.Log("Turret Health: " + totalHealth);
        
        if (totalHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (explosionEffect != null) 
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject); 
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= range)
        {
            // 1. Target the center of the player
            Vector3 targetPoint = player.position + Vector3.up * playerCenterOffset;
            Vector3 direction = targetPoint - turretHead.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // 2. Rotate Base (Y-axis only)
            Vector3 baseRotation = Quaternion.Lerp(turretBase.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
            turretBase.rotation = Quaternion.Euler(0f, baseRotation.y, 0f);

            // 3. Rotate Head with Offset
            float targetX = lookRotation.eulerAngles.x;
            Quaternion targetLocalRot = Quaternion.Euler(targetX + headRotationOffset, 0f, 0f);
            turretHead.localRotation = Quaternion.Lerp(turretHead.localRotation, targetLocalRot, Time.deltaTime * rotationSpeed);

            // 4. Shooting
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        // We spawn the laser
        GameObject laserGo = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        
        // OPTIONAL: If your laser script needs to know who shot it or how much damage to do,
        // you can grab a component from 'laserGo' here.
    }
}