using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private PaddleManager _paddleManager;
    public GameObject ball;

    [SerializeField] public float paddleSpeed = 1;
    [SerializeField] private float power = 10;

    [SerializeField] private BoolEventSO OnResetControllers;

    private bool isBallLaunched = false;
    private bool isInputEnabled = false;
    private Paddle selectedPaddle;

    private void OnEnable()
    {
        OnResetControllers.OnEventRaised += WaitForInput;
    }
    private void OnDisable()
    {
        OnResetControllers.OnEventRaised -= WaitForInput;
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }

            if (touch.phase.Equals(TouchPhase.Began))
            {
                InterationBegan();
            }
            InterationMoved(touch.position);

            if (touch.phase.Equals(TouchPhase.Ended))
            {
                InterationEnded();

            }
        }
        else
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (Input.GetMouseButton(0))
            {
                InterationMoved(Input.mousePosition);
            }
            if (Input.GetMouseButtonDown(0))
            {
                InterationBegan();
            }
            if (Input.GetMouseButtonUp(0))
            {
                InterationEnded();
            }
        }

    }
    private void FixedUpdate()
    {
        
    }
    private void InterationBegan()
    {
        if (!isBallLaunched)
        {
            LaunchBall();
        }

    }
    private void InterationMoved(Vector2 point)
    {
        if (isInputEnabled)
        {
            var interactionPoint = _mainCamera.ScreenToWorldPoint(point);
            interactionPoint.z = 0;

            if (selectedPaddle == null)
            {
                selectedPaddle = _paddleManager.GrabClosestPadlle(interactionPoint);
            }
            else
            {
                var direction = (interactionPoint - selectedPaddle.transform.localPosition);
                var dot = (selectedPaddle.transform.right * Vector2.Dot(selectedPaddle.transform.right, direction));
                var velocity = new Vector2(dot.x, dot.y) * paddleSpeed;
                selectedPaddle.MoveGroup(velocity);
            }
        }
       
    }
    private void InterationEnded()
    {
        if (selectedPaddle != null)
        {
            selectedPaddle.MoveGroup(Vector2.zero);
            selectedPaddle = null;
        }
           
    }

    private void WaitForInput(bool value)
    {
        isInputEnabled = false;
        isBallLaunched = false;
      
    }
    private void LaunchBall()
    {
        isInputEnabled = true;
        isBallLaunched = true;
        var randomAngle = Random.Range(-20,20);
        var dir = Quaternion.AngleAxis(randomAngle, Vector3.forward) * Vector3.up;
   
        ball.GetComponent<Rigidbody2D>().AddForce(dir * power);

    }
}
    
