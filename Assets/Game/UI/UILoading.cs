using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Foundation.DataAccess;
using Assets.Foundation.UI;
using Assets.Game.Project;
using Assets.Foundation.Extensions;

namespace Assets.Game.UI
{
    public class UILoading : UIFrame
    {
        public override string Path
        {
            get
            {
                return "loading/UILoading";
            }
        }

        private Slider loadProgress;

        private Image image;

        protected override void InitControls()
        {
            loadProgress = this.FindControl<Slider>("Slider");
            loadProgress.value = 0;
            loadProgress.RunAction(SliderTo.Create(20, 0, 1));

            image = this.FindControl<Image>("Image");
            var action = ImageProgressFromTo.Create(20, 0, 1);
            action.Shape = Image.FillMethod.Radial90;
            action.CircleOrigin = ImageProgress.OriginCircle.Bottom;
            image.RunAction(action);
        }

        protected override void InitLogic()
        {
            var workflow = new GameWorkflow();
            workflow.Start();
        }

        public override void InitWithParams(params object[] data)
        {
        }
    }
}
