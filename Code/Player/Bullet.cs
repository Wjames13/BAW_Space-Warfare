using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 2f;
    public int damage = 1; 

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet Trigger with: " + collision.name);
    }
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
