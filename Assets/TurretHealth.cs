using UnityEngine;

public class TurretHealth : MonoBehaviour
{
    public float health = 3f;
    public GameObject deathExplosionPrefab;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Turret hit! Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Die function called!");
        
        if (deathExplosionPrefab != null)
        {
            // Spawn explosion slightly above the turret's base
            Instantiate(deathExplosionPrefab, transform.position + Vector3.up, Quaternion.identity);
            Debug.Log("Explosion instantiated!");
        }
        else
        {
            Debug.LogError("NO EXPLOSION PREFAB ASSIGNED ON: " + gameObject.name);
        }

        Destroy(gameObject);
    }
}