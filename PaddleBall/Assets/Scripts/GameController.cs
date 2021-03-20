using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private PaddleManager _paddleManager;

    [SerializeField] private float paddleSpeed = 1;
    [SerializeField] private float power = 10;
    private Paddle selectedPaddle;
    public GameObject ball;
    private bool levelStarted = false;

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase.Equals(TouchPhase.Began))
                {
                    if (!levelStarted)
                    {
                        levelStarted = true;
                        ball.GetComponent<Rigidbody2D>().AddForce(transform.up * power, ForceMode2D.Impulse);
                    }
                                     
                }

                var touchPoint = _mainCamera.ScreenToWorldPoint(touch.position);


                touchPoint.z = 0;

                if (selectedPaddle == null)
                {

                    selectedPaddle = _paddleManager.GrabClosestPadlle(touchPoint);

                }
                else
                {
                    var direction = (touchPoint - selectedPaddle.transform.localPosition).normalized;
                    var dot = (selectedPaddle.transform.right * Vector2.Dot(selectedPaddle.transform.right, direction));
                    selectedPaddle.rigidbody2D.velocity = new Vector2(dot.x, dot.y) * Time.deltaTime * paddleSpeed;
                    //selectedPaddle.transform.localPosition += dot * paddleSpeed * Time.deltaTime;

                }
                if (touch.phase.Equals(TouchPhase.Ended))
                {
                    selectedPaddle.rigidbody2D.velocity = Vector2.zero;
                    selectedPaddle = null;

                }

            }
        }
       
    }
}
    
