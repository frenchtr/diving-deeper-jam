using System;
using OTStudios.DDJ.Runtime.Runtime.Bricks;
using UnityEngine;

namespace OTStudios.DDJ.Runtime.Runtime.Systems
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private BrickRegistry brickRegistry;
        
        private void Awake()
        {
            this.brickRegistry.Setup();
        }

        private void OnDestroy()
        {
            this.brickRegistry.Teardown();
        }
    }
}
