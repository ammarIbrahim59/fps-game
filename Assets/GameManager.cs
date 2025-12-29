using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    public int turretsToKill;
    public GameObject winPanel;
    public CanvasGroup winPanelGroup;

    private bool isGameWon = false; // Prevents WinGame from being called every single frame

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        if (winPanel != null) 
        {
            winPanel.SetActive(false);
            if (winPanelGroup != null) winPanelGroup.alpha = 0;
        }
    }

    public void TurretDestroyed() 
    {
        turretsToKill--;
        if (turretsToKill <= 0 && !isGameWon) WinGame();
    }

    void WinGame()
    {
        isGameWon = true; // Mark as won
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            if (winPanelGroup != null) winPanelGroup.alpha = 1;
            
            Time.timeScale = 0f; 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        // Manual override for testing
        if (Input.GetKeyDown(KeyCode.K)) WinGame();

        // FAILSAFE: If variable hits 0 OR if no turrets exist in hierarchy
        if (!isGameWon) 
        {
            // Condition 1: Check the counter
            if (turretsToKill <= 0) 
            {
                WinGame();
            }
            
            // Condition 2: Search the scene for anything with the Turret script
            // Replace 'TurretAimer' with the actual name of your turret script
            if (GameObject.FindObjectsOfType<TurretAimer>().Length == 0)
            {
                WinGame();
            }
        }
    }   
}