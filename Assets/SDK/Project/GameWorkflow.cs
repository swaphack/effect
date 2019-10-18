using System;
using System.Xml;
using UnityEngine;
using Assets.Foundation.DataAccess;
using Assets.Foundation.Tool;
using System.Collections.Generic;
using System.Collections;
using Assets.Foundation.Managers;

namespace Assets.SDK.Project
{
    /// <summary>
    /// 工作流程
    /// </summary>
    public class GameWorkflow : Workflow
    {      

        public GameWorkflow()
        {
            this.AddWork(new VersionCheckSlot());
            this.AddWork(new VersionUpdateSlot());
            this.AddWork(new LoginServerSlot());
        }

        /// <summary>
        /// 检查是否换包
        /// </summary>
        /// <returns></returns>
        public void Start()
        {
            EnumeratorManager.Instance.Add(this.UpdateWorkflow());
        }
    }
}
