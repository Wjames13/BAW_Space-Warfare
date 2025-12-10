using UnityEngine;

public class EnemyBullet1 : MonoBehaviour
{
    public float speed = 7f;
    public float lifetime = 5f;
    public int damage = 15;

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
