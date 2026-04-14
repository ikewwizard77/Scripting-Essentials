using UnityEngine;

public class SlamTrigger : MonoBehaviour
{
    [Header("Slam Settings")]
    public float slamSpeed = 20f;
    public float groundY = 0f;

    [Header("Optional")]
    public bool destroyOnGround = false;

    private bool isSlamming = false;

    void Update()
    {
        if (isSlamming)
        {
            SlamDown();
        }
    }

    void SlamDown()
    {
        Vector3 pos = transform.position;
        pos.y -= slamSpeed * Time.deltaTime;

        if (pos.y <= groundY)
        {
            pos.y = groundY;

            if (destroyOnGround)
            {
                Destroy(gameObject);
                return;
            }

            isSlamming = false; // remove if you want it to stay "active"
        }

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isSlamming = true;
        }
    }
}