using UnityEngine;


namespace Game.Foundation.Devices
{
    /// <summary>
    /// 键盘
    /// </summary>
    public class Keyboard : MonoBehaviour
    {
        void Update()
        {
            if (!Input.anyKeyDown && !Input.anyKey)
            {
                return;
            }

            if (Input.GetKey(KeyCode.Escape))
            {

            }
        }
    }
}
