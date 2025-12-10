using UnityEngine;

public class BossController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float leftLimit = -6f;
    public float rightLimit = 6f;

    private bool movingRight = true;
    private bool isDead = false;

    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;

    private float shootTimer;
    private BossStats stats;

    void Start()
    {
        stats = GetComponent<BossStats>();
    }

    void Update()
    {
        if (isDead) return;

        MovePattern();
        HandleShooting();
    }

    void MovePattern()
    {
        float step = moveSpeed * Time.deltaTime;

        if (movingRight)
            transform.position += Vector3.right * step;
        else
            transform.position += Vector3.left * step;

        if (transform.position.x >= rightLimit)
            movingRight = false;
        else if (transform.position.x <= leftLimit)
            movingRight = true;
    }

    void HandleShooting()
    {
        if (stats == null) return;

        shootTimer += Time.deltaTime;

        if (shootTimer >= stats.fireRate)
        {
            shootTimer = 1f;

            if (bulletPrefab != null)
            {
                if (firePoint1 != null)
                {
                    GameObject b1 = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
                    BossBullet bb1 = b1.GetComponent<BossBullet>();
                    if (bb1 != null) bb1.SetDamage(stats.damage);
                }

                if (firePoint2 != null)
                {
                    GameObject b2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
                    BossBullet bb2 = b2.GetComponent<BossBullet>();
                    if (bb2 != null) bb2.SetDamage(stats.damage);
                }
            }
        }
    }

    public void StopBoss()
    {
        isDead = true;
        this.enabled = false;
    }
}
