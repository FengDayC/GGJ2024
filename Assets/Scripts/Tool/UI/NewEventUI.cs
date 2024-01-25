using System;
using System.Collections;
using System.Collections.Generic;
using QxFramework.Core;
using UnityEngine;
using UnityEngine.UI;
using App.Common;

public class NewEventUI : UIBase
{
    [ChildValueBind("EventTitle", nameof(Text.text))]
    private string _eventTitle;

    [ChildValueBind("EventContent", nameof(Text.text))]
    private string _eventContent;

    //private string _resourcePath="Prefabs/UI/NewEventButton";
    [ChildComponentBind("Content")]
    private Transform _content;

    [ChildComponentBind("NewEventChooseButton")]
    private GameObject _resourceButton;

    [ChildValueBind("EventImage", nameof(Image.sprite.name))]
    private string _imagePath;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(_content);
        CommitValue();
    }

    public override void OnDisplay(object args)
    {
        var _chooseMessage = args as ChooseMessage;
        _eventTitle = _chooseMessage.EventTitle;
        _eventContent = _chooseMessage.EventContent;
        _imagePath = _chooseMessage.ImagePath;
        Debug.Log(_imagePath);
        SetChooseList(_chooseMessage.ChooseList);
        CommitValue();
    }

    private void SetChooseList(List<Choose> _chooseList)
    {
        //if (_chooseList.Count > _content.childCount)
        {
            //复制子物体
            for (int i = _content.childCount; i < _chooseList.Count; i++)
            {
                GameObject _newButton = Instantiate(_resourceButton, _content.transform) as GameObject;
            }
        }
        // else
        {
            //隐藏子物体
            for (int i = _chooseList.Count; i < _content.childCount; i++)
            {
                _content.GetChild(i).gameObject.SetActive(false);
            }
        }
        //子物体赋值
        for (int i = 0; i < _chooseList.Count; i++)
        {
            GameObject _newButton = _content.GetChild(i).gameObject;
            _newButton.SetActive(true);
            _newButton.transform.Find("ChooseButtonText").GetComponent<Text>().text = _chooseList[i].ChooseButton + "   <color= #EEEAC1>" + _chooseList[i].ChooseContent + "</color>";
            _newButton.transform.Find("ChooseButtonText").GetComponent<Text>().color = _chooseList[i].ActionBool ? new Color(0, 0, 0, 1) : Color.gray;
            int index = i;
            _newButton.GetComponent<Button>().onClick.SetListener(() =>
            {
                _chooseList[index].ChooseAction?.Invoke();
                UIManager.Instance.Close(this);
            }
            );
            _newButton.GetComponent<Button>().interactable = _chooseList[index].ActionBool;
        }
    }
}