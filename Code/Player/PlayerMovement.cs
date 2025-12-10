using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;
    public Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    public AudioSource audioSource;
    public AudioClip shootSFX;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float nextFireTime = 0f;
    public int maxAmmo = 100;
    public int currentAmmo;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI noAmmoText;
    public float noAmmoDisplayTime = 1.2f;
    private float noAmmoTimer = 0f;
    public int bulletDamage = 2;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null) animator.applyRootMotion = false;
        currentAmmo = maxAmmo;
        if (noAmmoText != null) noAmmoText.gameObject.SetActive(false);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (animator != null) animator.SetFloat("Horizontal", movement.x);

        if (ammoText != null)
            ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;

        if (noAmmoText != null && noAmmoText.gameObject.activeSelf)
        {
            noAmmoTimer -= Time.deltaTime;
            if (noAmmoTimer <= 0)
                noAmmoText.gameObject.SetActive(false);
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time >= nextFireTime)
        {
            if (currentAmmo <= 0)
            {
                ShowNoAmmo();
                return;
            }

            Shoot();
            currentAmmo--;
            nextFireTime = Time.time + fireRate;
        }
    }

    void FixedUpdate()
    {
        if (movement.sqrMagnitude > 1) movement = movement.normalized;
        if (rb != null) rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        if (audioSource != null && shootSFX != null)
            audioSource.PlayOneShot(shootSFX);

        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet b = bulletObj.GetComponent<Bullet>();

        PlayerStats ps = GetComponent<PlayerStats>();
        if (b != null)
        {
            if (ps != null) b.damage = ps.GetDamage();
            else b.damage = bulletDamage;
        }
    }

    void ShowNoAmmo()
    {
        if (noAmmoText == null) return;
        noAmmoText.gameObject.SetActive(true);
        noAmmoTimer = noAmmoDisplayTime;
    }
}
