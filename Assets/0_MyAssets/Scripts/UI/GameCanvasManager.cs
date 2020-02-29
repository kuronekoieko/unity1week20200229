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
    [SerializeField] HumanCountText[] humanCountTexts;

    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: ScreenState.GAME);

        this.ObserveEveryValueChanged(timer => Variables.timer)
            .Subscribe(_ => { SetTimeCountText(); })
            .AddTo(this.gameObject);

        foreach (KeyValuePair<HumanType, int> kvp in Variables.humanCountDic)
        {
            var humanCountText = humanCountTexts
                    .Where(h => h.humanType == kvp.Key)
                    .FirstOrDefault();
            if (humanCountText == null) { continue; }

            this.ObserveEveryValueChanged(humanCount => Variables.humanCountDic[kvp.Key])
                .Subscribe(humanCount => { humanCountText.text.text = humanCount.ToString(); })
                .AddTo(this.gameObject);
        }


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

    [System.Serializable]
    public class HumanCountText
    {
        public HumanType humanType;
        public Text text;
    }
}
