using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PaddleManager : MonoBehaviour
{
    //paddles amount
    private int paddlesAmount = 4;
    //radius of paddles position
    public float radius = 4;
    [SerializeField] private Paddle paddlePrefab;
    [HideInInspector] public List<Paddle> paddles = new List<Paddle>();
    public EdgeCollider2D box;

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
        }
    }

    private void Start()
    {
        //resize collider
        var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        var minSide = Mathf.Min(screenBounds.x, screenBounds.y);
        var colliderPoints = box.points;
        for (int i = 0; i < colliderPoints.Length; i++)
        {
            var point = colliderPoints[i];
            point.x *= minSide;
            point.y *= minSide;
            colliderPoints.SetValue(point, i);
        }
        box.points = colliderPoints;
        radius = minSide - minSide * 0.2f;
        SetupPaddles();
    }


    //paddles placed along circle, counterclock-wise, start from right
    private void SetupPaddles()
    {
        for (int i = 0; i < paddlesAmount; i++)
        {
            float angle = i * Mathf.PI * 2f / paddlesAmount;
            var point = Vector3.zero + (new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0));
            var inst = Instantiate(paddlePrefab, transform);

            inst.transform.position = new Vector3(point.x, point.y, 0);
            var direction = Vector3.zero - inst.transform.position;
            inst.startPosition = inst.transform.position;
            inst.gameObject.name = i.ToString();
           

            inst.transform.up = direction;
            paddles.Add(inst);
        }
    }
    //grab nearest paddle
    public Paddle GrabClosestPadlle(Vector3 point)
    {
        float distance = Mathf.Infinity;
        Paddle closestPadlle = null;
        for (int i = 0; i < paddles.Count; i++)
        {
           float distanceToPaddle = (paddles[i].transform.position - point).magnitude;
          
           if (distanceToPaddle < distance)
           {
               distance = distanceToPaddle;
               closestPadlle = paddles[i];
           }
        }

        return closestPadlle;
    }

    public void MovePaddle(Paddle selectedPaddle,float angle)
    {
        float step = Mathf.PI * 2 / paddles.Count;
        var nextposition = AnglePoint(angle, radius);

        var v = 0;
        selectedPaddle.transform.localPosition = nextposition;
        for (int i = 0; i < paddles.Count; i++)
        {
            if (paddles[i] != selectedPaddle)
            {
                v += 1;
                nextposition = AnglePoint(angle + step*v, radius);
                paddles[i].transform.localPosition = nextposition;
            }
        }
    }
    private Vector3 AnglePoint(float angle, float radius)
    {
        Vector3 result = Vector3.zero;
        var x = radius * Mathf.Cos(angle);
        var y = radius * Mathf.Sin(angle);
        result = new Vector3(x, y, 0);
        return result;
    }
   
}

