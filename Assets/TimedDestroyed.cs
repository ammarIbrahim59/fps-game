using UnityEngine;

public class TimedDestroyed : MonoBehaviour
{
    public float timeBeforeDestroy = 2f;

    void Start()
    {
        // This is the only Start method allowed!
        Destroy(gameObject, timeBeforeDestroy);
    }
}