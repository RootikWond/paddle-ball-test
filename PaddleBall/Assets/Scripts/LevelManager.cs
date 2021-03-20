using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject ball;
    [SerializeField] private IntEventSO OnBallDamaged;
    [SerializeField] private VoidEventSO OnLevelFailed;
    [SerializeField] private VoidEventSO OnLevelWin;
    private void OnEnable()
    {
        OnBallDamaged.OnEventRaised += ResetLevel;
        OnLevelFailed.OnEventRaised += RestartLevel;
        OnLevelWin.OnEventRaised += NextLevel;
    }
    private void OnDisable()
    {
        OnBallDamaged.OnEventRaised -= ResetLevel;
        OnLevelFailed.OnEventRaised -= RestartLevel;
        OnLevelWin.OnEventRaised -= NextLevel;
    }

    private void ResetLevel(int lifeAmount)
    {
        //Reset ball position
        //Reset paddle position
        //Wait for interactions
    }
    private void RestartLevel()
    {
        //Reset ball position
        //Reset paddle position
        //Wait for interactions
    }
    private void NextLevel()
    {
        //Load Next Level
        //Reset ball position
        //Reset paddle position
        //Wait for interactions
    }

}
