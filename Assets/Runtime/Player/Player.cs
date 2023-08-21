using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    internal InputManager   Input       { get; private set; }
    internal PlayerMovement Movement    { get; private set; }
    internal Rigidbody2D    Rigidbody   { get; private set; }
    internal Animator       Animator    { get; private set; }

    public void Enable(bool enable) {
        Movement.enabled = enable;
    }

    private void Awake() {

        T Get<T>() where T : Component {
            var t = GetComponent<T>();
            return t != null ? t : GetComponentInChildren<T>();
        }

        Input       = Get<  InputManager    >();
        Movement    = Get<  PlayerMovement  >();
        Rigidbody   = Get<  Rigidbody2D     >();
        Animator    = Get<  Animator        >();

        //Enable(true);
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

    protected Player            Player          => Get(p => p               );
    protected InputManager      Input           => Get(p => p.Input         );
    protected Rigidbody2D       Rigidbody       => Get(p => p.Rigidbody     );
    protected Animator          Animator        => Get(p => p.Animator      );

    //protected new void print(object message) {
    //    UI.Log(message.ToString());
    //    MonoBehaviour.print(message);
    //}
}
