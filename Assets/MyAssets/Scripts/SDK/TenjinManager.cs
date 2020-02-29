using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity SDK実装手順
/// https://docs.tenjin.com/ja/send-events/unity.html
/// </summary>
public class TenjinManager : MonoBehaviour
{
    string API_KEY = "WNXD6KTSZQEIXVNAMDXEWEZGD5YIKWHX";
    public static TenjinManager i;
    void Awake()
    {
        i = this;
    }
    
    public void OnStart()
    {
#if !UNITY_EDITOR
        BaseTenjin instance = Tenjin.getInstance(API_KEY);
        if (instance) instance.Connect();
#endif
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) { return; }
#if !UNITY_EDITOR
            BaseTenjin instance = Tenjin.getInstance(API_KEY);
            if (instance) instance.Connect();
#endif

    }
}
