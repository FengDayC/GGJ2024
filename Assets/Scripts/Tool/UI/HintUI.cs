using System;
using System.Collections;
using System.Collections.Generic;
using QxFramework.Core;
using UnityEngine;
using UnityEngine.UI;
using App.Common;

public class HintUI : UIBase
{
    public override void OnDisplay(object args)
    {
        CommonHintRegister();
        ClickWindowRegister();
        ChooseWindowRegister();
    }

    private void CommonHintRegister()
    {
        MessageManager.Instance.Get<HintType>().RegisterHandler(HintType.CommonHint, DebugHint);
    }

    private void ClickWindowRegister()
    {
        MessageManager.Instance.Get<HintType>().RegisterHandler(HintType.ClickWindow, DebugClick);
    }

    private void ChooseWindowRegister()
    {
        MessageManager.Instance.Get<HintType>().RegisterHandler(HintType.ChooseWindow, DebugChoose);
    }

    private void DebugChoose(System.Object sender, EventArgs arg)
    {
        ChooseMessage _tempChoose = (ChooseMessage)arg;
        UIManager.Instance.Open("NewEventUI", args: _tempChoose);
    }

    private void DebugHint(System.Object sender, EventArgs arg)
    {
        //if(sender is MainUI)
        {
            HintMessage _tempHint = (HintMessage)arg;
            GameObject _hintObject = Instantiate(ResourceManager.Instance.Load<GameObject>("Prefabs/UI/_hintUI"), this.transform);
            _hintObject.transform.Find("_hintText").GetComponent<Text>().text = _tempHint.Content;
            StartCoroutine(HintShow(_hintObject));
        }
    }

    private void DebugClick(System.Object sender, EventArgs arg)
    {
        ClickMessage _tempClick = (ClickMessage)arg;
        string _title = _tempClick.Title;
        string _content = _tempClick.Content;
        UIManager.Instance.Open("DialogWindowUI", args: new DialogWindowUI.DialogWindowUIArg(_title, _content, null, "确定"
                  , () =>
                  {
                  }));
    }

    private IEnumerator HintShow(GameObject _originalObject)
    {
        float _increment = 0;
        while (_increment<1)
        {
            Color _textColor = _originalObject.transform.Find("_hintText").GetComponent<Text>().color;
            if (!UIManager.Instance.FindUI("WaitTime"))
            {
                _increment += Time.deltaTime * 0.02f;
                _originalObject.transform.SetPositionY(_originalObject.transform.position.y + 3f);
                _originalObject.transform.Find("_hintText").GetComponent<Text>().color = new Color(_textColor.r, _textColor.g, _textColor.b, _textColor.a - _increment);
                _originalObject.GetComponent<Image>().color = _textColor;
            }
            yield return 0;
        }
        Destroy(_originalObject);
    }
}