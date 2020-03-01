using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;

/// <summary>
/// ゲーム画面
/// ゲーム中に表示するUIです
/// あくまで例として実装してあります
/// ボタンなどは適宜編集してください
/// </summary>
public class GameCanvasManager : BaseCanvasManager
{
    [SerializeField] Text timerText;
    [SerializeField] Text humanCountText;
    [SerializeField] Text[] nPCHumanCountTexts;

    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: ScreenState.GAME);

        this.ObserveEveryValueChanged(timer => Variables.timer)
            .Subscribe(_ => { SetTimeCountText(); })
            .AddTo(this.gameObject);

        this.ObserveEveryValueChanged(humanCount => Variables.humanCount)
            .Subscribe(humanCount => { humanCountText.text = humanCount.ToString(); })
            .AddTo(this.gameObject);


        test(0);
        test(1);
        test(2);
    }

    void test(int i)
    {
        this.ObserveEveryValueChanged(nPCHumanCount => Variables.nPCHumanCounts[i])
            .Subscribe(nPCHumanCount => { nPCHumanCountTexts[i].text = Variables.nPCHumanCounts[i].ToString(); })
            .AddTo(this.gameObject);
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

    void SetTimeCountText()
    {
        int sec = Mathf.CeilToInt(Variables.timer);
        float mSec = (Variables.timer - (sec - 1)) * 60f;
        if (Variables.timer == Values.TIME_LIMIT) { mSec = 0; }
        timerText.text = sec + "." + mSec.ToString("00");
    }
}
