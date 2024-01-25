using QxFramework.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏管理器，用于管理之前由MonoSingleton所有逻辑
/// </summary>
public class GameMgr : MonoSingleton<GameMgr>
{
    /// <summary>
    /// 所有模块列表
    /// </summary>
    private readonly List<ModulePair> _modules = new List<ModulePair>();

    public static bool Enable;

    /// <summary>
    /// 初始化所有模块
    /// </summary>
    public void InitModules()
    {
        _modules.Clear();
        Add<IGameMapModule>(new GameMapModule(), ModuleEnum.GameMapModule);
    }

    public static T Get<T>()
    {
        var type = typeof(T);
        var pair = Instance._modules.Find((m) => m.ModuleType == type);
        if (pair == null)
        {
            Debug.Log("[GameMgr]未注册的模块" + type.Name);

            return default(T);
        }
        if (pair.Initialized == false)
        {
            try
            {
                pair.Module.Awake();
                pair.Initialized = true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        return (T)(object)pair.Module;
    }

    private void Awake()
    {
        //InitModules();
        Enable = true;
    }

    private void Add<T>(LogicModuleBase module, ModuleEnum moduleEnum)
    {
        _modules.Add(new ModulePair(typeof(T), module, moduleEnum));
        try
        {
            module.Init();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void Update()
    {
        if(!Enable)
        {
            return;
        }
        for (int i = 0; i < _modules.Count; i++)
        {
            var module = _modules[i];
            try
            {
                module.Module.Update();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    private void FixedUpdate()
    {
        if(!Enable)
        {
            return;
        }
        for (int i = 0; i < _modules.Count; i++)
        {
            var module = _modules[i];
            try
            {
                module.Module.FixedUpdate();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _modules.Count; i++)
        {
            var module = _modules[i];
            try
            {
                module.Module.OnDestroy();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        Enable = false;
    }

    private class ModulePair
    {
        public readonly Type ModuleType;
        public readonly LogicModuleBase Module;
        public ModuleEnum TypeEnum;

        public bool Initialized;

        public ModulePair(Type moduleType, LogicModuleBase module, ModuleEnum typeEnum)
        {
            ModuleType = moduleType;
            Module = module;
            TypeEnum = typeEnum;
        }
    }
}

public enum ModuleEnum
{
    Unknow = 0,
    GameMapModule,
}