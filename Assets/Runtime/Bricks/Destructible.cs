using System;
using UnityEngine;

namespace OTStudios.DDJ.Runtime.Runtime.Bricks
{
    public class Destructible : MonoBehaviour
    {
        public event Action Destroyed;

        public void Destroy()
        {
            this.Destroyed?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
