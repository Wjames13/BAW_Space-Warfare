using UnityEngine;

public class BossTakeDamageOnTrigger : MonoBehaviour
{
    public BossStats stats;
    public BossHealthHandler handler;

    void Start()
    {
        if (stats == null) stats = GetComponentInParent<BossStats>();
        if (handler == null) handler = GetComponentInParent<BossHealthHandler>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet")) return;

        Bullet b = other.GetComponent<Bullet>();
        if (b == null) return;

        stats.TakeDamage(b.damage);
        Destroy(other.gameObject);
    }
}
