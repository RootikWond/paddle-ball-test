using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleManager : MonoBehaviour
{
    //paddles amount
    private int paddlesAmount = 4;
    //radius of paddles position
    [SerializeField] private float radius = 4;
    [SerializeField] private Paddle paddlePrefab;
    [HideInInspector] public List<Paddle> paddles = new List<Paddle>();


    [SerializeField] private BoolEventSO OnResetControllers;

    private void OnEnable()
    {
        OnResetControllers.OnEventRaised += ResetPaddles;

    }
    private void OnDisable()
    {
        OnResetControllers.OnEventRaised -= ResetPaddles;
    }

    private void ResetPaddles(bool value)
    {
        foreach (Paddle paddle in paddles)
        {
            paddle.transform.localPosition = paddle.startPosition;
            paddle.rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void Start()
    {
        SetupPaddles();
    }
    //paddles placed along circle, counterclock-wise, start from right
    private void SetupPaddles()
    {
        for (int i = 0; i < 4; i++)
        {
            float angle = i * Mathf.PI * 2f / paddlesAmount;
            var point = Vector3.zero + (new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0));
            var inst = Instantiate(paddlePrefab, transform);
            
            inst.transform.position = new Vector3(point.x, point.y, 0);
            var direction = Vector3.zero - inst.transform.position;
            inst.startPosition = inst.transform.position;

            inst.transform.up = direction;
            paddles.Add(inst);
        }

        //set paddle neighbor for simultaneous movement
        paddles[0].SetNeighbor(paddles[2]);
        paddles[1].SetNeighbor(paddles[3]);


    }
    //grab nearest paddle
    public Paddle GrabClosestPadlle(Vector3 point)
    {
        float distance = Mathf.Infinity;
        Paddle closestPadlle = null;
        for (int i = 0; i < paddles.Count; i++)
        {
            float distanceToPaddle = (paddles[i].transform.position - point).sqrMagnitude;
            if(distanceToPaddle < distance)
            {
                distance = distanceToPaddle;
                closestPadlle = paddles[i];
            }
        }
        return closestPadlle;
    }
}
