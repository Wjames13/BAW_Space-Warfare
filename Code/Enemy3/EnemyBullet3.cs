using UnityEngine;

public class EnemyBullet3 : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 5f;
    public int damage = 30;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
