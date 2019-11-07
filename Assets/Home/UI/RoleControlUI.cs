using Game.Foundation.Controller;
using Game.Foundation.Extensions;
using Game.Foundation.UI;
<<<<<<< HEAD
=======
using Game.Home.Logic;
using System.Collections;
using System.Collections.Generic;
>>>>>>> eca791581e64b360c5edaa8138c8ad2da80cf39b
using UnityEngine;

namespace Game.Home.UI
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
            this.BindPressEvent("Forward", MoveForward, this.EndAcceleration);
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
            _target.CreateComponent<DirectionController>().Acceleration += 0.01f;
            _target.CreateComponent<DirectionController>().MoveForward();
        }

        protected void MoveBack()
        {
            if (_target == null)
            {
                return;
            }
            _target.CreateComponent<DirectionController>().Reset();
        }

        protected void EndAcceleration()
        {
            _target.CreateComponent<DirectionController>().Acceleration = 0;
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
