using UnityEngine;

public class TurretPartCollision : MonoBehaviour
{
    // Drag the RapidFireCannon (the parent with the TurretAimer script) here
    public TurretAimer mainTurretScript; 

    private void OnTriggerEnter(Collider other)
    {
        // Ensure this string matches your new tag exactly
        if (other.CompareTag("PlayerLaser")) 
        {
            mainTurretScript.TakeDamage(1);
            Destroy(other.gameObject); 
        }
    }
}