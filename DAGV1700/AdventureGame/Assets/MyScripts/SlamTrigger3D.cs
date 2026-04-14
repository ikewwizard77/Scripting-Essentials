using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class SlamTrigger3D : MonoBehaviour
{
    [Header("Slam Settings")]
    public float slamSpeed = 20f;
    public float groundY = 0f;
    public bool slamOnlyOnce = true;

    private bool isSlamming = false;
    private bool hasSlammed = false;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Make sure trigger physics works reliably
        rb.isKinematic = true;
        rb.useGravity = false;

        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;
    }

    void Update()
    {
        if (!isSlamming)
            return;

        Vector3 pos = transform.position;
        pos.y -= slamSpeed * Time.deltaTime;

        if (pos.y <= groundY)
        {
            pos.y = groundY;
            isSlamming = false;
            hasSlammed = true;
        }

        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name + " | Tag: " + other.tag);

        if (slamOnlyOnce && hasSlammed)
            return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER DETECTED - SLAM START");
            isSlamming = true;
        }
    }
}