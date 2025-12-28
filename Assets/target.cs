using UnityEngine;

public class Target : MonoBehaviour
{
    // Turret dies in 3 hits
    public int health = 3;

    // We removed 'float amount' so the gun doesn't get confused
    public void Hit()
    {
        health--; // Subtracts 1 from health

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // This counts the death in your LevelManager
        if (LevelManager.instance != null)
        {
            LevelManager.instance.RemoveTurret();
        }
        
        Destroy(gameObject);
    }
}