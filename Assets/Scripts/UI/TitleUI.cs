using QxFramework.Core;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : UIBase
{
    public override void OnDisplay(object args)
    {
        base.OnDisplay(args);
        Get<Button>("EndButton").onClick.AddListener(OnEndButtonClicked);
        Get<Button>("StartButton").onClick.AddListener(OnStartButtonClicked);
    }

    private void OnEndButtonClicked()
    {
        Application.Quit();
    }

    private void OnStartButtonClicked()
    {

    }
}
