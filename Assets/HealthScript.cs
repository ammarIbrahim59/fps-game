using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; 

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    
    [Header("Lives Settings")]
    public static int lives = 3; 
    public TextMeshProUGUI livesText; 

    [Header("UI References")]
    public Slider healthSlider;
    public Image vignetteImage;
    public GameObject gameOverUI;
    public TextMeshProUGUI gameOverMessage; 
    public Button respawnButton; // Drag your Respawn Button here

    private bool isDead = false;

    void Start()
    {
        Time.timeScale = 1f;
        currentHealth = maxHealth;
        
        UpdateUI();

        if (gameOverUI != null) gameOverUI.SetActive(false);
        
        if (vignetteImage != null)
        {
            Color c = vignetteImage.color;
            c.a = 0f; 
            vignetteImage.color = c;
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (healthSlider != null) healthSlider.value = currentHealth;

        UpdateVignette();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }

    void UpdateVignette()
    {
        if (vignetteImage != null)
        {
            float healthPercent = currentHealth / maxHealth;
            float alphaValue = (1f - healthPercent) * 0.6f; 
            Color tempColor = vignetteImage.color;
            tempColor.a = alphaValue;
            vignetteImage.color = tempColor;
        }
    }

        void Die()
    {
        if (isDead) return;
        isDead = true;

        lives -= 1; 
        Time.timeScale = 0f; 

        // Update the lives text immediately so it doesn't still say "3"
        if (livesText != null) livesText.text = "Lives: " + lives;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (gameOverMessage != null) 
            {
                // TURN THE TEXT ON ONLY NOW
                gameOverMessage.gameObject.SetActive(true); 

                if (lives <= 0)
                {
                    gameOverMessage.text = "GAME OVER\nNO LIVES LEFT";
                    if (respawnButton != null) respawnButton.gameObject.SetActive(false);
                }
                else
                {
                    gameOverMessage.text = "YOU DIED!";
                    if (respawnButton != null) respawnButton.gameObject.SetActive(true);
                }
            }
        }
    }

    public void Respawn()
    {
        if (lives > 0)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Call this if you want to reset the whole game
    public void ResetGame()
    {
        lives = 3;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}