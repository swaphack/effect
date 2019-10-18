using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Assets.Foundation.Extensions;
using Assets.Foundation.Effects;
using UnityEngine.UI;

namespace Assets.Editor.Windows
{
    class RenderExtensions
    {
        [MenuItem("GameObject/Effect/Snapshot", priority = 11)]
        private static void AddSnapshot()
        {
            GameObject activeGameObject = Selection.activeGameObject;
            if (activeGameObject == null)
            {
                return;
            }

            var renderer = activeGameObject.GetComponent<Renderer>();
            if (renderer == null)
            {
                Debug.LogErrorFormat("GameObject [{0}] not has Renderer Component", renderer.name);
                return;
            }

            Material mat = new Material(Shader.Find("Unlit/Texture"));
            renderer.material = mat;

            GameObject go = new GameObject();
            Camera.main.gameObject.AddChild(go);

            MaterialSnapshot snapshot = go.AddComponent<MaterialSnapshot>();
            snapshot.Material = mat;

            go.name = string.Format("{0} {1}", renderer.name, snapshot.GetType().Name);
            go.transform.localPosition = Vector3.zero;
            go.transform.localEulerAngles = Vector3.zero;
        }

        [MenuItem("GameObject/Effect/UI Snapshot", priority = 11)]
        private static void AddUISnapshot()
        {
            GameObject activeGameObject = Selection.activeGameObject;
            if (activeGameObject == null)
            {
                return;
            }

            var image = activeGameObject.GetComponent<Image>();
            if (image == null)
            {
                Debug.LogErrorFormat("GameObject [{0}] not has image Component", image.name);
                return;
            }

            Material mat = new Material(Shader.Find("Unlit/Texture"));
            image.material = mat;

            GameObject go = new GameObject();
            Camera.main.gameObject.AddChild(go);

            ImageSnapshot snapshot = go.AddComponent<ImageSnapshot>();
            snapshot.Image = image;

            go.name = string.Format("{0} {1}", image.name, snapshot.GetType().Name);
            go.transform.localPosition = Vector3.zero;
            go.transform.localEulerAngles = Vector3.zero;
        }

        [MenuItem("GameObject/Effect/Mirror", priority = 11)]
        private static void AddMirror()
        {
            GameObject activeGameObject = Selection.activeGameObject;
            if (activeGameObject == null)
            {
                return;
            }

            var renderer = activeGameObject.GetComponent<Renderer>();
            if (renderer == null)
            {
                Debug.LogErrorFormat("GameObject [{0}] not has Renderer Component", renderer.name);
                return;
            }

            Material mat = new Material(Shader.Find("Unlit/Texture"));
            renderer.material = mat;

            GameObject go = new GameObject();
            Mirror mirror = go.AddComponent<Mirror>();
            mirror.Material = mat;

            go.name = string.Format("{0} {1}", renderer.name, mirror.GetType().Name);
            go.transform.localPosition = Vector3.zero;
            activeGameObject.AddChild(go);
        }
    }
}
