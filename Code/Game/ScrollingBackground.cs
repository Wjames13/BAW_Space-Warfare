using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    public float speed = 2f;
    private float height;

    void Start()
    {
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y <= -height)
        {
            transform.position += new Vector3(0, height * 2f, 0);
        }
    }
}
