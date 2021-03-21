using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    
    [SerializeField] private Image[] ballLives;
    
    [SerializeField] private IntEventSO UpdateLives;
    [Header("Test buttons events")]
    [SerializeField] private BoolEventSO OnResetControllers;
    [SerializeField] private VoidEventSO OnLevelWin;
    [SerializeField] private FloatEventSO ChangeBallSpeed;

    private void OnEnable()
    {
         UpdateLives.OnEventRaised += UpdateLivesUI;
    }
    private void OnDisable()
    {

        UpdateLives.OnEventRaised -= UpdateLivesUI;
    }

    private void UpdateLivesUI(int value)
    {
        if (value >= ballLives.Length)
        {

            for (int i = 0; i < ballLives.Length; i++)
            {
                SetImageAlpha(ballLives[i], 1f);
            }
            return;
        }
        //Get image at index
        SetImageAlpha(ballLives[value], 0.2f);
    }

    private void SetImageAlpha(Image image, float alpha)
    {
        var color = image.color;
        color.a = alpha;
        image.color = color;
    }

    public void IncreaseSpeed()
    {
        ChangeBallSpeed.RaiseEvent(1f);
    }
    public void DecreaseSpeed()
    {
        ChangeBallSpeed.RaiseEvent(-1f);
    }
    public void NextLevel()
    {
        OnLevelWin.RaiseEvent();
        OnResetControllers.RaiseEvent(true);
    }
}
