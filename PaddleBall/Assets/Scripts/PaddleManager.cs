using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleManager : MonoBehaviour
{
    //paddles amount
    private int paddlesAmount = 6;
    //radius of paddles position
    [SerializeField] private float radius = 4;
    [SerializeField] private Paddle paddlePrefab;
    [HideInInspector] public List<Paddle> paddles = new List<Paddle>();
    //holder for 2 paddles
    public PaddleHolder paddleHolder;
    private void Start()
    {
        SetupPaddles();
    }
    //paddles placed along circle, counterclock-wise, start from right
    private void SetupPaddles()
    {
        if (paddlesAmount > 0)
        {
            for (int i = 0; i < paddlesAmount; i++)
            {
                float angle = i * Mathf.PI * 2f / paddlesAmount;
                var point = Vector3.zero + (new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0));
                var inst = Instantiate(paddlePrefab, transform);

                inst.transform.position = new Vector3(point.x, point.y, 0);
                var direction = Vector3.zero - inst.transform.position;
                inst.transform.up = direction;
                paddles.Add(inst);
            }
        }

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
