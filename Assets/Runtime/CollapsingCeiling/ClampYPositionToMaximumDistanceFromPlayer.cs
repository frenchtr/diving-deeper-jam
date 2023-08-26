using System;
using UnityEngine;

namespace OTStudios.DDJ.Runtime.Runtime.CollapsingCeiling
{
    public class ClampYPositionToMaximumDistanceFromPlayer : MonoBehaviour
    {
        [SerializeField]
        private Player player;
        [SerializeField]
        private float distance = 20f;
        
        private void Update()
        {
            var thisPosition = this.transform.position;
            var playerPosition = this.player.transform.position;
            
            if (Mathf.Abs(playerPosition.y - this.transform.position.y) > this.distance)
            {
                this.transform.position = new Vector3()
                {
                    x = thisPosition.x,
                    y = playerPosition.y + this.distance,
                    z = thisPosition.z,
                };
            }
        }
    }
}
