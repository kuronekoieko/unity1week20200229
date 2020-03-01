using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create NPCSkinSO", fileName = "NPCSkinSO")]
public class NPCSkinSO : ScriptableObject
{
    public Transform[] skins;

    private static NPCSkinSO _i;
    public static NPCSkinSO i
    {
        get
        {
            string PATH = "ScriptableObjects/" + nameof(NPCSkinSO);
            //初アクセス時にロードする
            if (_i == null)
            {
                _i = Resources.Load<NPCSkinSO>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_i == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _i;
        }
    }
}

