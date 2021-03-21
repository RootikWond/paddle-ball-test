using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int playerHP = 3;
    [SerializeField] private IntEventSO OnBallDestroyed;
    [SerializeField] private IntEventSO OnBlockDestroyed;
    [SerializeField] private VoidEventSO OnLevelFailed;
    [SerializeField] private VoidEventSO OnLevelWin;
    [SerializeField] private BoolEventSO OnResetControllers;
    [SerializeField] private IntEventSO UpdateLives;
    private void OnEnable()
    {
        OnBallDestroyed.OnEventRaised += BallDestroyed;
        OnBlockDestroyed.OnEventRaised += BlockDestroyed;
       
    }
    private void OnDisable()
    {
        OnBallDestroyed.OnEventRaised -= BallDestroyed;
        OnBlockDestroyed.OnEventRaised -= BlockDestroyed;
    }

    private void BallDestroyed(int value)
    {
        playerHP -= value;
        OnResetControllers.RaiseEvent(true);
        if (playerHP > 0)
        {
            
            UpdateLives.RaiseEvent(playerHP);
        } else
        {
            playerHP = 3;
            OnLevelFailed.RaiseEvent();
            UpdateLives.RaiseEvent(playerHP);
            OnResetControllers.RaiseEvent(true);
        }
    }
        
    private void BlockDestroyed(int blocksAmount)
    {
        if (blocksAmount <= 0)
        {
            playerHP = 3;
            OnLevelWin.RaiseEvent();
            UpdateLives.RaiseEvent(playerHP);
            OnResetControllers.RaiseEvent(true);
        }
    }
    IEnumerator WaitForLevelReset(float time)
    {
        yield return new WaitForSeconds(time);
        

    }
    IEnumerator WaitForNextLevel(float time)
    {
        yield return new WaitForSeconds(time);
       

    }

}
