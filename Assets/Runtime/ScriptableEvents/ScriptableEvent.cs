using System;
using UnityEngine;
using UnityEngine.Events;

namespace OTStudios.DDJ.Runtime.Systems.ScriptableEvents
{
    [CreateAssetMenu(menuName = "Scriptables/ScriptableEvent")]
    public class ScriptableEvent : ScriptableObject
    {
        [SerializeField]
        private UnityEvent raised;
        
        public event Action Raised;
        
        public void Raise()
        {
            this.raised?.Invoke();
            this.Raised?.Invoke();
        }
    }
    
    public abstract class ScriptableEvent<TValue> : ScriptableObject
    {
        [SerializeField]
        private UnityEvent<TValue> raised;
        
        public event Action<TValue> Raised;
        
        public void Raise(TValue value)
        {
            this.raised?.Invoke(value);
            this.Raised?.Invoke(value);
        }
    }
}
