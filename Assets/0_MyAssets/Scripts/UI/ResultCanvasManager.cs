using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.SceneManagement;

/// <summary>
/// 結果画面
/// あくまで例として実装してあります
/// ボタンなどは適宜編集してください
/// </summary>
public class ResultCanvasManager : BaseCanvasManager
{
    [SerializeField] Button nextGameButton;
    [SerializeField] Button twitterButton;

    public override void OnStart()
    {
        nextGameButton.onClick.AddListener(OnClickNextGameButton);
        twitterButton.onClick.AddListener(OnClickTwitterButton);
        base.SetScreenAction(thisScreen: ScreenState.RESULT);
    }

    public override void OnInitialize()
    {
        gameObject.SetActive(false);
    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);
        // Type == Number の場合
        naichilab.RankingLoader.Instance
            .SendScoreAndShowRanking(Variables.humanCountDic[HumanType.Player]);
    }

    protected override void OnClose()
    {

    }

    void OnClickNextGameButton()
    {
        Variables.screenState = ScreenState.INITIALIZE;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnClickTwitterButton()
    {
        // AudioManager.i.PlayOneShot(1);
        string tweetText = "あなたのスコアは…\n\n" +
            "" + (Variables.humanCountDic[HumanType.Player]) +
            "\n\nでした！！みんなもやってみよう！！" +
            "\n\n#unitychanio\n#unity1week\n";
        try
        {
            naichilab.UnityRoomTweet.Tweet("unitychanio", tweetText);
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
