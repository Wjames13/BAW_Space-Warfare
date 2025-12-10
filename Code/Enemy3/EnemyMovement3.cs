using UnityEngine;

public class EnemyMovement3 : MonoBehaviour
{
    public float speed = 1.2f;
    public float bottomY = -6f;
    public float topY = 6f;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y <= bottomY)
        {
            Vector3 pos = transform.position;
            pos.y = topY;
            transform.position = pos;
        }
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }
}
