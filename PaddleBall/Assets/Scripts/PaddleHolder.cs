using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleHolder : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rigidbody2D;
    
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
