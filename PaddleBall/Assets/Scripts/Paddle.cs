using UnityEngine;

public class Paddle : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rigidbody2D;
    [HideInInspector] public Paddle neighbor;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
    }
}
