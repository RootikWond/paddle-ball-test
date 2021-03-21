using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public float maxSpeed = 20;
    public float minSpeed = 1;
    public float speed = 10;
    //private int ballHealth = 3;
    public int damage = 1;
    [SerializeField] private IntEventSO OnBallDamaged;
    [SerializeField] private FloatEventSO ChangeBallSpeed;
    [SerializeField] private BoolEventSO OnResetControllers;
    private Vector2 lastVelocity;

    private Vector2 startPosition;


    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }
    public void ResetTransform(bool value)
    {
        transform.position = startPosition;
        rigidbody2D.velocity = Vector2.zero;
    }
    private void OnEnable()
    {
        ChangeBallSpeed.OnEventRaised += AdjustBallSpeed;
        OnResetControllers.OnEventRaised += ResetTransform;
    }
    private void OnDisable()
    {
        ChangeBallSpeed.OnEventRaised -= AdjustBallSpeed;
        OnResetControllers.OnEventRaised -= ResetTransform;

    }
  
    private void AdjustBallSpeed(float value)
    {
       
        if (rigidbody2D.velocity.magnitude == 0)
        {
            rigidbody2D.velocity = lastVelocity;
        }
        lastVelocity = rigidbody2D.velocity;
        speed = Mathf.Clamp(speed + value,0,maxSpeed);
    }
    private void FixedUpdate()
    {
        rigidbody2D.velocity = rigidbody2D.velocity.normalized * speed;
        //rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, maxSpeed);//control max ball speed
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Border")))
        {

            OnBallDamaged.RaiseEvent(1);
              
        }
    }

}
