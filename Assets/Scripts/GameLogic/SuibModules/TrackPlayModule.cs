using SonicBloom.Koreo;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TrackPlayModule : LogicModuleBase, ITrackPlayModule
{
    private Koreographer _koreographer;

    public override void Init()
    {
        base.Init();
        _koreographer = GameObject.Find("Koreographer").GetComponent<Koreographer>();

    }
}

public interface ITrackPlayModule
{

}
