using QxFramework.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleProcedure : ProcedureBase
{
    protected override void OnEnter(object args)
    {
        base.OnEnter(args);
        UIManager.Instance.Open("TitleUI", 0);
    }
}
