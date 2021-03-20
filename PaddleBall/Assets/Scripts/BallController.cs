using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public int lifeAmount = 3;
    public int damage = 1;
    [SerializeField] private IntEventSO OnBallDamaged;
    [SerializeField] private FloatEventSO ChangeBallSpeed;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        ChangeBallSpeed.OnEventRaised += AdjustBallSpeed;
    }
    private void OnDisable()
    {
        ChangeBallSpeed.OnEventRaised -= AdjustBallSpeed;
    }

    private void AdjustBallSpeed(float value)
    {
        rigidbody2D.velocity *= value;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Border")))
        {
            OnBallDamaged.RaiseEvent(lifeAmount);
            if (lifeAmount <= 0)
            {
                //gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Paddle")))
        {
            rigidbody2D.velocity *= 1.1f;
            rigidbody2D.AddForce(rigidbody2D.velocity.normalized * 2, ForceMode2D.Impulse);
        }
    }
}
