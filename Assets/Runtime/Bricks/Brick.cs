using UnityEngine;

namespace OTStudios.DDJ.Runtime.Runtime.Bricks
{
    public class Brick : MonoBehaviour
    {
        [SerializeField]
        private BrickRegistry registry;
        [SerializeField]
        private Destructible destructible;

        private void OnEnable()
        {
            this.registry.Register(this);
        }

        private void OnDisable()
        {
            this.registry.Deregister(this);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.gameObject.name == "Player")
            {
                foreach (var contact in collision.contacts)
                {
                    var dot = Vector2.Dot(Vector2.up, contact.normal);

                    if (dot < -0.8f)
                    {
                        this.destructible.Destroy();
                        return;
                    }
                }
            }
        }
    }
}
