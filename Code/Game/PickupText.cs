using UnityEngine;

public class PickupText : MonoBehaviour
{
    public enum PickupType { Speed, FireRate, Health, Damage, Ammo }
    public PickupType type;
    public GameObject floatingTextPrefab;
    public float speedAmount = 1.2f;
    public float maxSpeed = 15f;

    public float fireRateReduce = 0.1f;
    public float minFireRate = 0.15f;

    public int healthAmount = 50;

    public int ammoAmountPercent = 50;

    public int damageAmount = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        PlayerStats ps = other.GetComponent<PlayerStats>();

        switch (type)
        {
            case PickupType.Speed:
                pm.moveSpeed = Mathf.Min(pm.moveSpeed + speedAmount, maxSpeed);
                ShowFloatingText("+Speed");
                break;

            case PickupType.FireRate:
                pm.fireRate = Mathf.Clamp(pm.fireRate - fireRateReduce, minFireRate, 10f);
                ShowFloatingText("+FireRate");
                break;

            case PickupType.Health:
                ph.currentHealth = Mathf.Min(ph.currentHealth + healthAmount, ph.maxHealth);
                ph.UpdateHealthUI();
                ShowFloatingText("+50 Health");
                break;

            case PickupType.Damage:
                ps.AddDamage(damageAmount);
                ShowFloatingText("+Damage");
                break;

            case PickupType.Ammo:
                int amount = pm.maxAmmo * ammoAmountPercent / 100;
                pm.currentAmmo = Mathf.Min(pm.currentAmmo + amount, pm.maxAmmo);
                ShowFloatingText("+50 Ammo");
                break;
        }
        void ShowFloatingText(string msg)
        {
            if (floatingTextPrefab != null)
            {
                GameObject go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
                go.GetComponent<FloatingTextUI>().SetText(msg);
            }
        }


        Destroy(gameObject);
    }
}
