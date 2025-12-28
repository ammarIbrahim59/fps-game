using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public float speed = 100f; // Adjust this in the Inspector to change travel speed
    public float lifeTime = 5f; 
    private bool hasHit = false; // Prevents the 'MissingReference' error

    void Start()
    {
        // Keep the hierarchy clean by deleting missed shots
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Only move if we haven't hit anything yet
        if (!hasHit)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasHit) return; // Exit if already hit something

        // Check if we hit a turret
        Target target = other.GetComponentInParent<Target>();
        
        if (target != null)
        {
            target.Hit();
        }

        hasHit = true; 
        Destroy(gameObject); // Remove the laser from the scene
    }
}