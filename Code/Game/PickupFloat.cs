using UnityEngine;

public class PickupFloat : MonoBehaviour
{
    public float floatSpeed = 0.6f;

    void Update()
    {
        transform.position += Vector3.down * floatSpeed * Time.deltaTime;
    }
}
