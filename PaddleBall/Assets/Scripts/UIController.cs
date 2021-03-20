using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public FloatEventSO ChangeBallSpeed;

    public void IncreaseSpeed()
    {
        ChangeBallSpeed.RaiseEvent(1.1f);
    }
    public void DecreaseSpeed()
    {
        ChangeBallSpeed.RaiseEvent(0.9f);
    }
}
