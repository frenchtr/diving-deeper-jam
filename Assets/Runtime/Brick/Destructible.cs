using System;
using UnityEngine;

namespace OTStudios.DDJ.Runtime.Runtime.Brick
{
    public class Destructible : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.gameObject.name == "Player")
            {
                foreach (var contact in collision.contacts)
                {
                    var dot = Vector2.Dot(Vector2.up, contact.normal);
                    Debug.Log(dot);
                    
                    if (dot < -0.8f)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
