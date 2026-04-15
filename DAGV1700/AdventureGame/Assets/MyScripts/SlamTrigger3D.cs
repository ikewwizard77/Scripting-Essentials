using UnityEngine;


public enum SlamState { FLOAT, SLAM, RETURN }

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class SlamTrigger3D : MonoBehaviour
{
    [Header("Slam Settings")]
    public float slamSpeed = 20f;
    public float riseSpeed = 10f;
    public float groundY = 0f;
    public bool slamOnlyOnce = true;

    public SlamState state = SlamState.FLOAT;

    //private bool isSlamming = false;
    private bool hasSlammed = false;
    private Rigidbody rb;

    public Vector3 intialStart;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Make sure trigger physics works reliably
        rb.isKinematic = true;
        rb.useGravity = false;

        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;
    }

    private void Start()
    {
        intialStart = transform.position;
    }

    void Update()
    {


        if(state == SlamState.FLOAT)
        {
            
        }

        if(state == SlamState.SLAM)
        {
            Vector3 pos = transform.position;
        pos.y -= slamSpeed * Time.deltaTime;

        if (pos.y <= groundY)
        {
            pos.y = groundY;
            state = SlamState.RETURN;
            hasSlammed = true;
        }

        transform.position = pos;
        }

        if(state == SlamState.RETURN)
        {
            Vector3 pos = transform.position;
            pos.y += riseSpeed * Time.deltaTime;

            if(pos.y >= intialStart.y)
            {
                pos.y = intialStart.y;
                state = SlamState.FLOAT;
            }

            transform.position = pos;
        }



        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name + " | Tag: " + other.tag);

        if (slamOnlyOnce && hasSlammed)
            return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER DETECTED - SLAM START");
            state = SlamState.SLAM;
        }
    }
}