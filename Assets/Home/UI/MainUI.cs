using Assets.Foundation.UI;
using Assets.Home.Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Home.UI
{
    public class MainUI : UIFile
    {
        public override string Path
        {
            get
            {
                return "Main/MainUI";
            }
        }

        private Text _food;
        private Text _wood;
        private Text _iron;
        private Text _silver;

        protected override void InitControls()
        {
            _food = FindControl<Text>("Food");
            _wood = FindControl<Text>("Wood");
            _iron = FindControl<Text>("Iron");
            _silver = FindControl<Text>("Silver");
        }

        private void UpdateResource()
        {
            _food.text = Role.Resource.food.ToString();
            _wood.text = Role.Resource.wood.ToString();
            _iron.text = Role.Resource.iron.ToString();
            _silver.text = Role.Resource.silver.ToString();
 
        }

        protected override void InitLogic()
        {
            Role.Load();

            this.UpdateResource();
        }


        void Update()
        {
            Role.Resource.food += 1;
            Role.Resource.wood += 1;
            Role.Resource.iron += 1;
            Role.Resource.silver += 1;

            this.UpdateResource();
            Role.Save();
        }

        public override void InitWithParams(params object[] data)
        {
        }
    }
}
