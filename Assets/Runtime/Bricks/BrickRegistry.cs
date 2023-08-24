using System.Collections.Generic;
using TravisRFrench.Common.Runtime.Registration;
using UnityEngine;

namespace OTStudios.DDJ.Runtime.Runtime.Bricks
{
    [CreateAssetMenu(menuName = "Scriptables/Registries/Brick")]
    public class BrickRegistry : ScriptableRegistry<Brick>
    {
        public IEnumerable<Brick> Bricks => ((IRegistrar<Brick>)this).Entities;
    }
}
