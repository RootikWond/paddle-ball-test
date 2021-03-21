using UnityEngine;

public class Paddle : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rigidbody2D;
    [HideInInspector] public Paddle neighbor;
    [HideInInspector] public Vector2 startPosition;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
    }
    
    public void SetNeighbor(Paddle newNeighbor)
    {
        neighbor = newNeighbor;
        newNeighbor.neighbor = this;
    }
    public void MoveGroup(Vector2 velocity)
    {
        rigidbody2D.velocity = velocity;
        if (neighbor != null)
        {
            neighbor.rigidbody2D.velocity = velocity;
        }
    }
 }
