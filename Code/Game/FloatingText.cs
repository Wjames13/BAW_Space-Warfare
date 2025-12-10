using UnityEngine;
using TMPro;

public class FloatingTextUI : MonoBehaviour
{
    public float lifetime = 1f;
    public float moveUpSpeed = 20f;
    public TextMeshProUGUI text;

    void Update()
    {
        transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
            Destroy(gameObject);
    }

    public void SetText(string msg)
    {
        text.text = msg;
    }
}
