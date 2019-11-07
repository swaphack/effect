using UnityEngine;

namespace Game.Foundation.Events
{
    public interface IScrollProtocol
    {
        GameObject Target { get; }
        void DoScale(float delta);
    }
}
