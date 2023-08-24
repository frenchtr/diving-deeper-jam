using System;
using UnityEngine;

namespace OTStudios.DDJ.Runtime.Runtime.Bricks
{
    public class Destructible : MonoBehaviour
    {
        public event Action Destroyed;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.gameObject.name == "Player")
            {
                foreach (var contact in collision.contacts)
                {
                    var dot = Vector2.Dot(Vector2.up, contact.normal);

                    if (dot < -0.8f)
                    {
                        this.Destroy();
                    }
                }
            }
        }

        public void Destroy()
        {
            this.Destroyed?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
