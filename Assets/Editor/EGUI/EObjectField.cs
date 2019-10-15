using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EObjectField : Widget
    {
        private UnityEngine.Object _target;
        private Type _targetType;
        private bool _allowSceneObjects;

        public UnityEngine.Object Target
        {
            get { return _target; }

            set { _target = value; }
        }

        public Type TargetType
        {
            get { return _targetType; }

            set { _targetType = value; }
        }

        public bool AllowSceneObjects
        {
            get { return _allowSceneObjects; }

            set { _allowSceneObjects = value; }
        }

        public EObjectField()
        {
            AllowSceneObjects = true;
        }

        protected override void OnDraw()
        {
            UnityEngine.Object obj = EditorGUILayout.ObjectField(Content, Target, TargetType, AllowSceneObjects, Option.Values);
            if (obj != Target)
            {
                Target = obj;
                this.DipatchEvent();
            }
        }
    }
}
