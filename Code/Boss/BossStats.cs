using UnityEngine;

public class BossStats : MonoBehaviour
{
    public int health = 1000;
    public int damage = 30;
    public float fireRate = 0.6f;

    public int CurrentHealth => health;
    public int scoreValue = 500;
    public void TakeDamage(int amount)
    {
        health -= amount;
        health = Mathf.Max(health, 0);
        BossHealthHandler b = GetComponent<BossHealthHandler>();
        if (b != null)
        {
            b.CheckDeath();
        }
    }


}
