using UnityEngine;

public class LaserBolt : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f; // Despawn after 3 seconds so they don't fly forever

    void Start()
    {
        // Tell the laser to move forward based on its own rotation
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        
        // Destroy the laser automatically after a few seconds
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If it hits anything that isn't the turret itself, destroy it
        if (other.gameObject.name != "Turretbase")
        {
            // You can add an explosion effect here later!
            Destroy(gameObject);
        }
    }
}