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
                InteractionBegan();
            }
            InterationRotationMoved(touch.position);

        }
        else
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
            
                InteractionBegan();
               

            }

            if (Input.GetMouseButton(0))
            {
                   InterationRotationMoved(Input.mousePosition);
            }
       
        }

    }

    private void InteractionBegan()
    {
        var interactionPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        selectedPaddle = _paddleManager.GrabClosestPadlle(interactionPoint);
        if (!isBallLaunched)
        {
            LaunchBall();
        }

    }

    private void InterationRotationMoved(Vector2 point)
    {
        if (isInputEnabled)
        {
            var interactionPoint = _mainCamera.ScreenToWorldPoint(point);
            Vector3 dir = interactionPoint - Vector3.zero;

            float angle = Mathf.Atan2(dir.y, dir.x);
        
            if (selectedPaddle != null)
            {
                _paddleManager.MovePaddle(selectedPaddle, angle);
            }
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
    
