using UnityEngine;
using UnityEngine.Events;

namespace OTStudios.DDJ.Runtime.Systems.ScriptableEvents
{
    public class ScriptableEventListener : MonoBehaviour
    {
        [SerializeField]
        private ScriptableEvent @event;
        [SerializeField]
        public UnityEvent response;

        private void OnEnable()
        {
            this.@event.Raised += OnEventRaised;
        }
        
        private void OnDisable()
        {
            this.@event.Raised -= OnEventRaised;
        }

        private void OnEventRaised()
        {
            this.response.Invoke();
        }
    }

    public abstract class ScriptableEventListener<TValue> : MonoBehaviour
    {
        [SerializeField]
        private ScriptableEvent<TValue> @event;
        [SerializeField]
        public UnityEvent<TValue> response;

        private void OnEnable()
        {
            this.@event.Raised += OnEventRaised;
        }
        
        private void OnDisable()
        {
            this.@event.Raised -= OnEventRaised;
        }

        private void OnEventRaised(TValue value)
        {
            this.response.Invoke(value);
        }
    }
}
