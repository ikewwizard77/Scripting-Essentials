using UnityEngine;
using UnityEngine.AI;
public class NPCFlipper : MonoBehaviour
{
    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    
    }
    void Update()
    {
        // Only flip if the agent is actually moving horizontally
        float xVel = agent.velocity.x;
        
        if (Mathf.Abs(xVel) > 0.01f)
        {
            // Flip when moving left
            spriteRenderer.flipX = xVel < 0f;
        }
    }
}
