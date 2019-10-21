using UnityEngine;

namespace Assets.Foundation.Events
{
    public interface IScrollProtocol
    {
        GameObject Target { get; }
        void DoScale(float delta);
    }
}
