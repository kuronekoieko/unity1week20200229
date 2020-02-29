using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// ゲーム画面
/// ゲーム中に表示するUIです
/// あくまで例として実装してあります
/// ボタンなどは適宜編集してください
/// </summary>
public class GameCanvasManager : BaseCanvasManager
{
    [SerializeField] Button gameEndButton;
    public override void OnStart()
    {
        gameEndButton.onClick.AddListener(OnClickGameEndButton);
        base.SetScreenAction(thisScreen: ScreenState.GAME);
    }

    public override void OnInitialize()
    {
        gameObject.SetActive(true);
    }

    protected override void OnOpen()
    {

    }

    protected override void OnClose()
    {
        gameObject.SetActive(false);
    }

    void OnClickGameEndButton()
    {
        Variables.screenState = ScreenState.RESULT;
    }
}
