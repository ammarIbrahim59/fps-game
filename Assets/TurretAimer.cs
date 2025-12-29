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
    public float rotationSpeed = 3f; 
    public float fireRate = 1f;
    public float playerCenterOffset = 1f; 

    [Header("Model Alignment")]
    public float headRotationOffset = 7f; 

    [Header("Health System")]
    public int totalHealth = 3;
    public GameObject explosionEffect;
    
    [Header("Audio")]
    public AudioClip explosionSound;
    private AudioSource shootSound;

    private float fireCountdown = 0f;
    private GameManager gameManager; // Cached reference

    void Start()
    {
        shootSound = GetComponent<AudioSource>();
        
        // Find the manager once when the turret is created
        gameManager = Object.FindAnyObjectByType<GameManager>();
    }

    public void TakeDamage(int damage)
    {
        totalHealth -= damage;
        Debug.Log(gameObject.name + " Health: " + totalHealth);
        
        if (totalHealth <= 0)
        {
            Die();
        }
    }

        // Inside your TurretAimer or Health script
    void Die()
    {
        // 1. Tell the GameManager a turret was destroyed
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TurretDestroyed();
        }
        else
        {
            // This will tell you if the script can't find the Manager
            Debug.LogError("Turret can't find GameManager Instance!");
        }

        // 2. Play effects and destroy the turret
        Debug.Log("Die function called!"); 
        Destroy(gameObject); 
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= range)
        {
            Vector3 targetPoint = player.position + Vector3.up * playerCenterOffset;
            Vector3 direction = targetPoint - turretHead.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            Vector3 baseRotation = Quaternion.Lerp(turretBase.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
            turretBase.rotation = Quaternion.Euler(0f, baseRotation.y, 0f);

            float targetX = lookRotation.eulerAngles.x;
            Quaternion targetLocalRot = Quaternion.Euler(targetX + headRotationOffset, 0f, 0f);
            turretHead.localRotation = Quaternion.Lerp(turretHead.localRotation, targetLocalRot, Time.deltaTime * rotationSpeed);

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
        if (laserPrefab != null && firePoint != null)
        {
            Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        }
        
        if (shootSound != null)
        {
            shootSound.pitch = Random.Range(0.9f, 1.1f);
            shootSound.Play();
        }
    }
}