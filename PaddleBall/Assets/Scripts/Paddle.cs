using UnityEngine;

public class Paddle : MonoBehaviour
{
    [HideInInspector] public Vector2 startPosition;
    private void Update()
    {
        transform.up = Vector3.zero - transform.localPosition;
    }

}
