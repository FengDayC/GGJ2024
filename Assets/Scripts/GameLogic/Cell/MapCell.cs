using System.Collections;
using System.Collections.Generic;
using TbsFramework.Cells;
using UnityEngine;

public class MapCell : Square
{
    private Vector3 _cellDimension = Vector3.one;
    public override Vector3 GetCellDimensions()
    {
        return _cellDimension;
    }
}
