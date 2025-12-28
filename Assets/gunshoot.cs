using UnityEngine;
using System.Collections;

public class Gunshoot : MonoBehaviour
{
    public float range = 500f;
    public float speed = 150f; 
    public Camera fpsCam;
    public LineRenderer laserLine;
    public Transform muzzle; // NEW: The point at the tip of your gun
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
            
            // This is what makes the turret die!
            Target target = hit.transform.GetComponent<Target>();
            if (target != null) target.Hit();
        }
        else
        {
            // If you miss, the laser travels far into the sky
            endPos = fpsCam.transform.position + (fpsCam.transform.forward * range);
        }

        float distance = Vector3.Distance(muzzle.position, endPos);
        float duration = distance / speed; // Speed should be about 50 in Inspector
        float timer = 0;

         while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            
            // This line keeps the laser pinned to the gun even while you move
            laserLine.SetPosition(0, muzzle.position); 
            
            // This line calculates the travel path toward the target
            laserLine.SetPosition(1, Vector3.Lerp(muzzle.position, endPos, progress));
            yield return null;
        }

        laserLine.SetPosition(1, endPos); 
        yield return new WaitForSeconds(0.15f); // Stay visible briefly so you see the hit
        laserLine.enabled = false;
    }
}