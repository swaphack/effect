using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Foundation.Extensions;
using UnityEngine.UI;

namespace Game.Foundation.UI
{
    public class UIScrollText : UIListView
    {
        private UIText Text
        {
            get
            {
                if (content == null)
                {
                    return null;
                }
                return content.GetComponentInChildren<UIText>();
            }
        }

        public UIScrollText()
        {
            Direction = ScrollDirection.Vertical;
        }

        private bool _bInitTextEvent;

        private void UpdateTextEvent()
        {
            if (Text == null)
            {
                return;
            }

            if (_bInitTextEvent) 
            {
                return;
            }

            _bInitTextEvent = true;
            Text.OnTextChanged += (UIText text) =>
            {
                this.UpdateScrollView();
            };
        }

        protected override void UpdateScrollView()
        {
            UpdateTextEvent();

            base.UpdateScrollView();
        }

        protected override void SetLayoutParams(HorizontalOrVerticalLayoutGroup layout)
        {
            if (layout == null)
            {
                return;
            }

            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;
        }
    }
}
