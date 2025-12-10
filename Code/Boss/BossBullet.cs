using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 7f;
    public float lifetime = 6f;

    private int damage;

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth p = other.GetComponent<PlayerHealth>();
            if (p != null) p.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
