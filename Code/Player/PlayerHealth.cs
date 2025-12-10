using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    [HideInInspector] public int currentHealth;

    public HealthBarUI healthUI;
    public Image healthFill;
    
    public AudioSource audioSource;
    public AudioClip deathSFX;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth < 0) currentHealth = 0;

        Debug.Log("Player Damaged HP: " + currentHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("PLAYER DIED");


        if (audioSource != null && deathSFX != null)
            audioSource.PlayOneShot(deathSFX);

        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("isDead", true);
        }


        PlayerMovement move = GetComponent<PlayerMovement>();
        if (move != null) move.enabled = false;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;


        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Static;
        }
        GameManager.instance.GameOver();
        Invoke(nameof(DisablePlayer), 1.2f);
    }

    void DisablePlayer()
    {
        gameObject.SetActive(false);
    }

  

    public void UpdateHealthUI()
    {
        UpdateHealthBar();

        if (healthUI != null)
            healthUI.UpdateHealthUI();
    }

    public void UpdateHealthBar()
    {
        if (healthFill != null)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthFill.fillAmount = fillAmount;
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
