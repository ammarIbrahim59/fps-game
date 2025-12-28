using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    void Start()
    {
        // This removes the explosion object after 2 seconds
        Destroy(gameObject, 2f);
    }
}