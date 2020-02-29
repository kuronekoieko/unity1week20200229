using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartCanvasManager : BaseCanvasManager
{
    [SerializeField] Button startButton;
    [SerializeField] Text startButtonText;
    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: ScreenState.START);
        gameObject.SetActive(true);
        startButton.onClick.AddListener(OnClickStartButton);
    }

    public override void OnInitialize()
    {
    }

    protected override void OnOpen()
    {
        Anim();
    }

    protected override void OnClose()
    {
        gameObject.SetActive(false);
    }

    void OnClickStartButton()
    {
        Variables.screenState = ScreenState.GAME;
    }

    void Anim()
    {
        Sequence sequence = DOTween.Sequence()
        .Append(startButtonText.rectTransform.DOScale(1.1f, 0.5f))
        .Append(startButtonText.transform.DOScale(1f, 0.5f))
        .OnComplete(() =>
        {
            Anim();
        });

    }

}
