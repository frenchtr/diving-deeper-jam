using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OTStudios.DDJ.Runtime.Runtime.Bricks;

namespace OTStudios.DDJ.Runtime {

    public class Player : MonoBehaviour {

        [field: SerializeField] internal AudioReferences    AudioReferences { get; private set; }
        [field: SerializeField] internal BrickRegistry      BrickRegistry   { get; private set; }
        [field: SerializeField] internal InputManager       Input           { get; private set; }
        [field: SerializeField] internal PlayerMovement     Movement        { get; private set; }
        [field: SerializeField] internal Rigidbody2D        Rigidbody       { get; private set; }
        [field: SerializeField] internal Animator           Animator        { get; private set; }
        [field: SerializeField] internal PlayerHealth       Health          { get; private set; }
        [field: SerializeField] internal PlayerUI           UI              { get; private set; }
        [field: SerializeField] internal GameOverMenu       GameOverMenu    { get; private set; }

        public void Enable(bool enable) {
            Movement.enabled = enable;

            if (!enable) Rigidbody.velocity = Vector2.zero;
        }
    }

    public abstract class PlayerComponent : MonoBehaviour {

        private Player _player;
        private T Get<T>(System.Func<Player, T> get) {

            if (_player == null) {
                _player = GetComponent<Player>();
                if (_player == null) _player = GetComponentInParent<Player>();
            }

            return get.Invoke(_player);
        }

        protected Player            Player          => Get(p => p                   );
        protected PlayerMovement    Movement        => Get(p => p.Movement          );
        protected InputManager      Input           => Get(p => p.Input             );
        protected Rigidbody2D       Rigidbody       => Get(p => p.Rigidbody         );
        protected Animator          Animator        => Get(p => p.Animator          );
        protected PlayerHealth      Health          => Get(p => p.Health            );
        protected PlayerUI          UI              => Get(p => p.UI                );
        protected BrickRegistry     BrickRegistry   => Get(p => p.BrickRegistry     );
        protected AudioReferences   AudioReferences => Get(p => p.AudioReferences   );
        protected GameOverMenu      GameOverMenu    => Get(p => p.GameOverMenu      );

        //protected new void print(object message) {
        //    UI.Log(message.ToString());
        //    MonoBehaviour.print(message);
        //}
    }
}
