using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 1f; // Lower damage so it takes a few hits to explode
    public float lifeTime = 3f;

    [Header("Effects")]
    public GameObject impactExplosionPrefab; 

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if we hit a Turret
        if (other.CompareTag("Turret"))
        {
            // Search the object we hit for the TurretHealth script
            TurretHealth healthScript = other.GetComponent<TurretHealth>();

            if (healthScript != null)
            {
                healthScript.TakeDamage(damage);
            }
            else
            {
                // If the script is on the PARENT, find it there
                healthScript = other.GetComponentInParent<TurretHealth>();
                if (healthScript != null) healthScript.TakeDamage(damage);
            }

            Destroy(gameObject); // Destroy laser on hit
            return;
        }

        // 2. Damage Player
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null) player.TakeDamage(10);
            Destroy(gameObject);
        }
    }
}