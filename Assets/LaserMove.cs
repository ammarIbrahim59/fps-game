using UnityEngine;

public class LaserMove : MonoBehaviour {
    public float speed = 40f;
    public float lifeTime = 3f;

    void Start() {
        Destroy(gameObject, lifeTime);
    }

    void Update() {
    // This moves the laser WITHOUT using physics, so it won't fall!
    transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}