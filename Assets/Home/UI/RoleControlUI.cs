using Assets.Foundation.Controller;
using Assets.Foundation.Extensions;
using Assets.Foundation.UI;
using Assets.Home.Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Home.UI
{
    public class RoleControlUI : UIFile
    {
        [SerializeField]
        private GameObject _target;

        public override string Path
        {
            get
            {
                return "Common/RoleControl";
            }
        }
        protected override void InitControls()
        {
            this.BindPressEvent("Left", TurnLeft);
            this.BindPressEvent("Right", TurnRight);
            this.BindPressEvent("Forward", MoveForward);
            this.BindPressEvent("Back", MoveBack);
        }

        protected void TurnLeft()
        {
            if (_target == null)
            {
                return;
            }

            _target.CreateComponent<DirectionController>().TurnLeft();
        }

        protected void TurnRight()
        {
            if (_target == null)
            {
                return;
            }

            _target.CreateComponent<DirectionController>().TurnRight();
        }

        protected void MoveForward()
        {
            if (_target == null)
            {
                return;
            }

            _target.CreateComponent<DirectionController>().MoveForward();
        }

        protected void MoveBack()
        {
            if (_target == null)
            {
                return;
            }

            _target.CreateComponent<DirectionController>().MoveBack();
        }

        protected override void InitLogic()
        {
        }

        public override void InitWithParams(params object[] data)
        {
            if (data == null || data.Length == 0)
            {
                return;
            }
            _target = (GameObject)data[0];
        }
    }
}
