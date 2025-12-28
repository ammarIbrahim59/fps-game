using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int turretsToDestroy = 3;

    void Awake()
    {
        instance = this;
    }

    public void RemoveTurret()
    {
        turretsToDestroy--;
        Debug.Log("Turret destroyed! Remaining: " + turretsToDestroy);

        if (turretsToDestroy <= 0)
        {
            Debug.Log("All Turrets Gone! You Win!");
            // We will add the Win Screen trigger here next
        }
    }
}