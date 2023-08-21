using System;
using UnityEngine;

namespace OTStudios.DDJ.Runtime.Runtime.Brick
{
    public class Destructible : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(this.gameObject);
        }
    }
}
