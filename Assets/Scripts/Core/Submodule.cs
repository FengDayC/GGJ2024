using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QxFramework.Core
{
    /// <summary>
    /// 子类模块
    /// </summary>
    public class Submodule 
    {
        /// <summary>
        /// 父对象
        /// </summary>
        public Submodule ModuleParent;

        /// <summary>
        /// 关联流程
        /// </summary>
        public ProcedureBase ProcedureRoot;

        /// <summary>
        /// 子对象
        /// </summary>
        public List<Submodule> Children = new List<Submodule>();

        public List<UIBase> OpenUIs = new List<UIBase>();

        /// <summary>
        /// 设置Root
        /// </summary>
        /// <param name="procedureRoot"></param>
        public void SetRootProcedure(ProcedureBase procedureRoot)
        {
            ProcedureRoot = procedureRoot;
        }

        /// <summary>
        /// 设置Root
        /// </summary>
        /// <param name="procedureRoot"></param>
        /// <param name="subcomponentRoot"></param>
        private void SetRoot(ProcedureBase procedureRoot, Submodule submoduleParent = null)
        {
            ModuleParent = submoduleParent;
            if (submoduleParent == null)
            {
                submoduleParent = this;
            }
            ProcedureRoot = procedureRoot;
        }

        /// <summary>
        /// 打开UI
        /// </summary>
        /// <param name="uiName"></param>
        /// <param name="layer"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        protected UIBase OpenUI(string uiName, string name = "", object args = null)
        {
            var uibase = UIManager.Instance.Open(uiName, name:name, args:args);
            OpenUIs.UniqueAdd(uibase);
            return uibase;
        }



        /// <summary>
        ///  关闭指定名称的UI，是从后往前查找。
        /// </summary>
        /// <param name="uiName">UI名称。</param>
        protected void CloseUI(string uiName, string objName = "")
        {
            UIManager.Instance.Close(uiName, objName);

            //以防万一移除掉
            OpenUIs.RemoveAll((ui) => { return !ui.enabled; });         
        }

        /// <summary>
        /// 关闭打开的所有UI
        /// </summary>
        protected void CloseAllUI()
        {
            for (int i = 0; i < OpenUIs.Count; i++)
            {
                UIManager.Instance.Close(OpenUIs[i]);
            }
        }

        /// <summary>
        /// 添加子对象
        /// </summary>
        /// <param name="sub"></param>
        public void AddChilren(Submodule sub)
        {
            sub.SetRoot(sub.ProcedureRoot, this);
        }

        public void Init()
        {
            OnInit();
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Init();
            }
        }

        public void Update()
        {
            OnUpdate();
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Update();
            }
        }

        public void FixedUpdate()
        {
            OnFixedUpdate();
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].FixedUpdate();
            }
        }

        /// <summary>
        /// 销毁自身
        /// </summary>
        public void Destory()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Destory();
            }
            OnDestroy();
            MessageManager.Instance.RemoveAbout(this);
            CloseAllUI();
        }

        /// <summary>
        /// 销毁某个子对象
        /// </summary>
        /// <param name="submodule"></param>
        public void DestoryChidren(Submodule submodule)
        {
            if (Children.Contains(submodule))
            {
                submodule.Destory();
                Children.Remove(submodule);
            }
        }

        protected virtual void OnInit()
        {

        }

        protected virtual void OnUpdate()
        {

        }

        protected virtual void OnFixedUpdate()
        {

        }

        protected virtual void OnDestroy()
        {

        }        
    }
}