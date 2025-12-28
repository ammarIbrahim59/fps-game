using UnityEngine;
using System.Collections;

public class Gunshoot : MonoBehaviour
{
    public float range = 500f;
    public float speed = 150f; 
    public float damagePerShot = 1f; // How much damage your gun does
    public Camera fpsCam;
    public LineRenderer laserLine;
    public Transform muzzle; 
    public AudioSource shootSound;

    void Start()
    {
        if (laserLine != null) laserLine.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ShootLaser());
        }
    }

    IEnumerator ShootLaser()
    {
        if (shootSound != null) shootSound.Play();

        laserLine.enabled = true;
        Vector3 endPos;
        RaycastHit hit;

        // Shoots forward from your camera center
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            endPos = hit.point;
            
            // --- UPDATED LOGIC FOR TURRETHEALTH ---
            // 1. Look for the TurretHealth script on the object we hit
            TurretHealth turret = hit.transform.GetComponent<TurretHealth>();
            
            if (turret != null)
            {
                // 2. Deal damage to the turret
                turret.TakeDamage(damagePerShot);
            }
            else
            {
                // 3. Optional: Look in the parent object in case the collider is on a child
                turret = hit.transform.GetComponentInParent<TurretHealth>();
                if (turret != null) turret.TakeDamage(damagePerShot);
            }
        }
        else
        {
            endPos = fpsCam.transform.position + (fpsCam.transform.forward * range);
        }

        float distance = Vector3.Distance(muzzle.position, endPos);
        float duration = distance / speed; 
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            
            laserLine.SetPosition(0, muzzle.position); 
            laserLine.SetPosition(1, Vector3.Lerp(muzzle.position, endPos, progress));
            yield return null;
        }

        laserLine.SetPosition(1, endPos); 
        yield return new WaitForSeconds(0.15f); 
        laserLine.enabled = false;
    }
}