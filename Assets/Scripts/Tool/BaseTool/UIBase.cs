using QxFramework.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using App.Common;
using UnityEngine;

namespace QxFramework.Core
{
    /// <summary>
    /// 所有UI父对象的基类。
    /// </summary>
    public abstract class UIBase : MonoBehaviour
    {
        //用于存储和快速查找子物体
        public Dictionary<string, GameObject> _gos => _childBindTool.Gos;

        private ChildBindTool _childBindTool;

        /// <summary>
        /// 获得层级
        /// </summary>
        /// <returns></returns>
        public virtual int UILayer => -1;

        /// <summary>
        /// 当UI被显示时执行。
        /// </summary>
        public virtual void OnDisplay(object args)
        {
        }

        /// <summary>
        /// 当注册消息处理方法时执行。
        /// </summary>
        protected virtual void OnRegisterHandler()
        {
        }

        /// <summary>
        /// 当关闭时执行。
        /// </summary>
        protected virtual void OnClose()
        {
            //TODO 考虑改成隐藏
            //Destroy(gameObject);
            if (gameObject != null)
            {
                ObjectPool.Recycle(gameObject);
            }
            else
            {
                Debug.Log("Destroyed gameObject" + name);
            }
        }

        /// <summary>
        /// 建立一个包含所有子物体的字典
        /// </summary>
        public void CollectObject()
        {
            if (_childBindTool == null)
            {
                _childBindTool = new ChildBindTool(this, transform);
            }
            _childBindTool.CollectObject();
        }

       /// <summary>
       /// 注册消息
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="t"></param>
       /// <param name="callback"></param>
        protected void RegisterMessage<T>(T t, EventHandler<EventArgs> callback) where T : struct
        {
            MessageManager.Instance.Get<T>().RegisterHandler(t, callback);
        }
    
        /// <summary>
        /// 注册游戏数据更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="key"></param>
        protected void RegisterDataUpdate<T>(Action<GameDataBase> action, string key = "Default")
            where T : GameDataBase, new()
        {
            Data.Instance.RegisterUpdateListener<T>(action,key);
        }

        /// <summary>
        /// 一个快速获取的函组件数，以物体名为Key，相同值则直接覆盖
        /// </summary>
        /// <typeparam name="T">组件</typeparam>
        /// <param name="name">物体名。</param>
        /// <returns>组件对象</returns>
        /// <exception cref="System.ArgumentNullException">没找到组件。</exception>
        public T Get<T>(string name)
        {
            if (_childBindTool == null)
            {
                _childBindTool = new ChildBindTool(this, transform);
            }
            return _childBindTool.Get<T>(name);
        }

        /// <summary>
        /// 当关闭时会移除有关注册的消息
        /// </summary>
        protected void RemoveHandler()
        {
            MessageManager.Instance.RemoveAbout(this);
            Data.Instance.RemoveAbout(this);
        }

        /// <summary>
        /// 执行关闭行为流程
        /// </summary>
        public void DoClose()
        {
            OnClose();
            RemoveHandler();
        }

        public void CommitValue()
        {
            _childBindTool.CommitValue();
        }

        /// <summary>
        /// 执行显示行为流程
        /// </summary>
        public void DoDisplay(object args)
        {
            if (_childBindTool == null)
            {
                _childBindTool = new ChildBindTool(this, transform);
            }
            else
            {
                CollectObject();
            }
         
            OnDisplay(args);
            OnRegisterHandler();
            _childBindTool.CommitValue();
        }
    }
}