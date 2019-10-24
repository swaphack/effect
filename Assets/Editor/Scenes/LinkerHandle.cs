using Assets.Editor.Widgets;
using Assets.Foundation.Scene;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Scenes
{
    [CustomEditor(typeof(LinkBehaviour))]
    public class LinkerHandle : SceneHandle
    {
        private MonoBehaviour _behaviour;

        void OnEnable()
        {
            _behaviour = (MonoBehaviour)target;
        }

        protected override void InitSceneGUI(UIWidget layout)
        {
            EditorVerticalLayout vLayout = new EditorVerticalLayout();
            {
                vLayout.Add(new UILabelFieldWidget("Name", _behaviour.name));
                vLayout.Add(new UILabelFieldWidget("Tag", _behaviour.tag));
                vLayout.Add(new UIVector3FieldWidget("Local Position", _behaviour.transform.localPosition));
                vLayout.Add(new UIVector3FieldWidget("Local Rotation", _behaviour.transform.localEulerAngles));
                vLayout.Add(new UIVector3FieldWidget("Local Scale", _behaviour.transform.localScale));

                vLayout.Add(new UIVector3FieldWidget("World Position", _behaviour.transform.position));
                vLayout.Add(new UIVector3FieldWidget("World Rotation", _behaviour.transform.eulerAngles));
                vLayout.Add(new UIVector3FieldWidget("World Scale", _behaviour.transform.lossyScale));
            }
            layout.Add(vLayout);
        }

        protected override void InitSceneWidget(UIWidget layout)
        {
            Transform transform = _behaviour.transform;
            {
                layout.Add(new ArrowShape(transform.position + transform.right, transform.rotation * Quaternion.LookRotation(Vector3.right)));
                layout.Add(new ArrowShape(transform.position + transform.up, transform.rotation * Quaternion.LookRotation(Vector3.up)));
                layout.Add(new ArrowShape(transform.position + transform.forward, transform.rotation * Quaternion.LookRotation(Vector3.forward)));
            }
        }
    }
}
