using System.Collections;
using System.Collections.Generic;
using App.Common;
using UnityEngine;

public class LogicModuleBase
{
    public virtual void Init()
    {
    }

    protected bool RegisterData<T>(out T data, string key = "Default") where T : GameDataBase, new()
    {
        return Data.Instance.InitData<T>(out data, key);
    }

    protected void SetModify<T>(T data, string key = "Default") where T : GameDataBase, new()
    {
        Data.Instance.SetModify<T>(data, this, key);
    }

    public virtual void Awake()
    {
    }

    public virtual void Update()
    {
    }
    public virtual void LateUpdate()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void OnDestroy()
    {
    }
}