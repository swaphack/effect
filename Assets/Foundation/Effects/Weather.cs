using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Foundation.Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Weather : MonoBehaviour
    {
        void Start()
        {
            var sys = this.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule mainModule = sys.main;
            mainModule.gravityModifierMultiplier = 10.0f;
            mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
        }
    }
}
