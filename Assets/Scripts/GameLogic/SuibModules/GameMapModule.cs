using QxFramework.Core;
using System.Collections;
using System.Collections.Generic;
using TbsFramework.Grid;
using UnityEngine;

public enum GridPos
{
    NW,
    N,
    NE,
    W,
    Center,
    E,
    SW,
    S,
    SE,
}

public class GameMapModule : LogicModuleBase, IGameMapModule
{
    private CellGrid _cellGrid;
    public CellGrid CellGrid
    {
        get
        {
            return _cellGrid;
        }
    }

    public override void Init()
    {
        base.Init();
        GameObject.Find("CellGrid").GetComponent<CellGrid>(); 
    }

    public override void Update()
    {
        base.Update();
    }
}

public interface IGameMapModule
{
}
