using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCanvasManager : BaseCanvasManager
{
    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: ScreenState.START);
    }

    public override void OnInitialize()
    {
    }

    protected override void OnOpen()
    {
    }

    protected override void OnClose()
    {
    }
}
