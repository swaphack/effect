using Assets.Editor.Widgets;

namespace Assets.Editor.Tools.GameDesign
{
    public class ObjectsLayout : UIWindow
    {
        protected override void InitUI(UIWidget layout)
        {
            UITransformFieldWidget transform = new UITransformFieldWidget("GameObject", _transform);
            transform.OnValueChanged = (object value) => {
                _transform = (Transform)value;
            };
            layout.Add(transform);


            UIIntFieldWidget width = new UIIntFieldWidget("width", _width);
            width.OnValueChanged = (object value) => {
                _width = (int)value;
            };
            layout.Add(width);

            UIIntFieldWidget height = new UIIntFieldWidget("height", _height);
            height.OnValueChanged = (object value) =>
            {
                _height = (int)value;
            };
            layout.Add(height);

            UIIntSlideFieldWidget count = new UIIntSlideFieldWidget("count", _count);
            count.MinValue = 1;
            count.MaxValue = 100;
            count.OnValueChanged = (object value) =>
            {
                _count = (int)value;
            };
            layout.Add(count);

            BButton btn = new BButton();
            btn.Text = "Create";
            btn.TriggerHandler = (Widget w) =>
            {
                CreateGameObject();
            };
            layout.Add(btn);
        }
    }
}
