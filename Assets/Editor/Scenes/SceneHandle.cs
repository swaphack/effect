using Game.Editor.Widgets;
using UnityEditor;

namespace Game.Editor.Scenes
{
    public class SceneHandle : UnityEditor.Editor
    {
        /// <summary>
        /// 场景布局
        /// </summary>
        private UIWidget _sceneGUILayout;
            /// <summary>
        /// 场景物件
        /// </summary>
        private UIWidget _sceneWidgets;

        /// <summary>
        /// 布局是否有修改
        /// </summary>
        private bool _dirty;

        /// <summary>
        /// 布局是否有修改
        /// </summary>
        public bool Dirty
        {
            get
            {
                return _dirty;
            }
            set
            {
                _dirty = value;
            }
        }

        public SceneHandle()
        {
            _sceneGUILayout = new UIWidget();
            _sceneGUILayout.Direction = LayoutDirection.Vertical;
            _sceneWidgets = new UIWidget();
            _sceneWidgets.Direction = LayoutDirection.Vertical;
            Dirty = true;
        }

        protected virtual void OnSceneGUI()
        {
            if (Dirty)
            {
                _sceneGUILayout.Clear();
                _sceneWidgets.Clear();

                this.InitSceneGUI(_sceneGUILayout);

                this.InitSceneWidget(_sceneWidgets);

                Dirty = false;
            }

            this.ShowDisplay();

            this.DoSceneEvent();
        }

        /// <summary>
        /// 初始化场景ui
        /// </summary>
        /// <param name="layout"></param>
        protected virtual void InitSceneGUI(UIWidget layout)
        {

        }

        /// <summary>
        /// 初始化场景物件
        /// </summary>
        /// <param name="layout"></param>
        protected virtual void InitSceneWidget(UIWidget layout)
        {

        }

        /// <summary>
        /// 执行场景事件
        /// </summary>
        protected virtual void DoSceneEvent()
        { 
        }


        protected void ShowDisplay()
        {
            if (_sceneWidgets != null)
            {
                _sceneWidgets.Draw();
            }

            Handles.BeginGUI();

            if (_sceneGUILayout != null)
            {
                _sceneGUILayout.Draw();
            }

            Handles.EndGUI();
        }
    }
}
