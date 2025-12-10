using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public int maxHealth = 15;
    public int currentHealth;
    private bool isDead = false;
    public int bulletDamage = 15;
    public int scoreValue = 10;
    public GameObject[] pickupPrefabs;
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    public AudioSource audioSource;
    public AudioClip deathSFX;

    private Animator anim;

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
        GameObject b = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);

        EnemyBullet1 bullet = b.GetComponent<EnemyBullet1>();
        if (bullet != null)
        {
            bullet.damage = bulletDamage; 
        }
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
                anim.SetTrigger("EnemyDead");

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

       
        if (roll <= 0.50f)
        {
            Instantiate(GetPickup("Ammo"), transform.position, Quaternion.identity);
            return;
        }

        if (roll <= 0.80f)
        {
            Instantiate(GetPickup("Health"), transform.position, Quaternion.identity);
            return;
        }

        if (roll <= 0.85f)
        {
            Instantiate(GetPickup("Damage"), transform.position, Quaternion.identity);
            return;
        }

        if (roll <= 0.90f)
        {
            Instantiate(GetPickup("FireRate"), transform.position, Quaternion.identity);
            return;
        }

        if (roll <= 0.92f)
        {
            Instantiate(GetPickup("Speed"), transform.position, Quaternion.identity);
            return;
        }

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
