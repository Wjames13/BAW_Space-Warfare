using UnityEngine;

public class ShipWrap : MonoBehaviour
{
    public Rigidbody2D rb;

    void Update()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPos.x < 0f) viewportPos.x = 1f;
        else if (viewportPos.x > 1f) viewportPos.x = 0f;

        if (viewportPos.y < 0f) viewportPos.y = 1f;
        else if (viewportPos.y > 1f) viewportPos.y = 0f;

        Vector3 newPos = Camera.main.ViewportToWorldPoint(viewportPos);
        newPos.z = 0f;  

        rb.position = newPos;
    }
}
