using System;
using System.Xml;
using UnityEngine;
using Game.Foundation.DataAccess;
using Game.Foundation.Tool;
using System.Collections.Generic;
using System.Collections;
using Game.Foundation.Common;

namespace Game.SDK.Project
{
    /// <summary>
    /// 工作流程
    /// </summary>
    public class GameWorkflow : Workflow
    {      

        public GameWorkflow()
        {
            this.AddWorkSlot(new VersionCheckSlot());
            this.AddWorkSlot(new VersionUpdateSlot());
            this.AddWorkSlot(new LoginServerSlot());
        }

        /// <summary>
        /// 检查是否换包
        /// </summary>
        /// <returns></returns>
        public void Start()
        {
            this.MoveNext(null);
        }
    }
}
