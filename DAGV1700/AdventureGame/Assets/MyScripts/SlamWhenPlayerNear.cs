using UnityEngine;

public class SlamWhenPlayerNear : MonoBehaviour
{
    [Header("Player Detection")]
    public Transform player;
    public float triggerDistance = 5f;

    [Header("Slam Settings")]
    public float slamSpeed = 20f;
    public float groundY = -3f; // Y position where the sprite should stop

    [Header("Optional")]
    public bool destroyOnGround = false;

    private bool hasSlammed = false;

    void Update()
    {
        if (hasSlammed || player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= triggerDistance)
        {
            hasSlammed = true;
        }

        if (hasSlammed)
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
        }

        transform.position = pos;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
}