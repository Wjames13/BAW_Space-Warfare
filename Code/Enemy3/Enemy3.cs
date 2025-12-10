using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    private bool isDead = false;
    public int bulletDamage = 30;
    public int scoreValue = 100;
    public GameObject[] pickupPrefabs;
    public GameObject enemyBulletPrefab;

    private Animator anim;
    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;

    public AudioSource audioSource;
    public AudioClip deathSFX;

    public float fireRate = 2f;
    private float nextFireTime = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject b1 = Instantiate(enemyBulletPrefab, firePoint1.position, firePoint1.rotation);
        GameObject b2 = Instantiate(enemyBulletPrefab, firePoint2.position, firePoint2.rotation);
        GameObject b3 = Instantiate(enemyBulletPrefab, firePoint3.position, firePoint3.rotation);

        EnemyBullet3 bullet1 = b1.GetComponent<EnemyBullet3>();
        EnemyBullet3 bullet2 = b2.GetComponent<EnemyBullet3>();
        EnemyBullet3 bullet3 = b3.GetComponent<EnemyBullet3>();

        if (bullet1 != null) bullet1.damage = bulletDamage;
        if (bullet2 != null) bullet2.damage = bulletDamage;
        if (bullet3 != null) bullet3.damage = bulletDamage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
                TakeDamage(bullet.damage);

            Destroy(other.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;


            Collider2D col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

            EnemyDamagePlayer1 dmg = GetComponent<EnemyDamagePlayer1>();
            if (dmg != null) dmg.enabled = false;


            if (anim != null)
                anim.SetTrigger("Enemy3Dead");

            if (audioSource != null && deathSFX != null)
                audioSource.PlayOneShot(deathSFX);

            DropPickup();
            ScoreManager.instance.AddScore(scoreValue);

            EnemySpawner spawner = FindAnyObjectByType<EnemySpawner>();
            if (spawner != null)
                spawner.EnemyDied();

            Destroy(gameObject, 0.5f);
        }
    }

    void DropPickup()
    {
        if (pickupPrefabs.Length == 0) return;

        float roll = Random.value;

        // AMMO (50%)
        if (roll <= 0.50f)
        {
            Instantiate(GetPickup("Ammo"), transform.position, Quaternion.identity);
            return;
        }

        // HEALTH (30%)
        if (roll <= 0.80f)
        {
            Instantiate(GetPickup("Health"), transform.position, Quaternion.identity);
            return;
        }

        // DAMAGE (5%)
        if (roll <= 0.85f)
        {
            Instantiate(GetPickup("Damage"), transform.position, Quaternion.identity);
            return;
        }

        // FIRERATE (5%)
        if (roll <= 0.90f)
        {
            Instantiate(GetPickup("FireRate"), transform.position, Quaternion.identity);
            return;
        }

        // SPEED (2%)
        if (roll <= 0.92f)
        {
            Instantiate(GetPickup("Speed"), transform.position, Quaternion.identity);
            return;
        }

        // 8% = No drop
    }

    GameObject GetPickup(string type)
    {
        switch (type)
        {
            case "Speed": return pickupPrefabs[0];
            case "FireRate": return pickupPrefabs[1];
            case "Health": return pickupPrefabs[2];
            case "Damage": return pickupPrefabs[3];
            case "Ammo": return pickupPrefabs[4];
        }
        return null;
    }

}
