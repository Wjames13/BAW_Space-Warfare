using UnityEngine;

public class EnemyDamagePlayer1 : MonoBehaviour
{
    public int damageToPlayer = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damageToPlayer);

                WarningUI warning = FindAnyObjectByType<WarningUI>();
                if (warning != null)
                    warning.ShowWarning("Capt.William: Don't get too close to the enemy!");
            }
        }
    }
}
