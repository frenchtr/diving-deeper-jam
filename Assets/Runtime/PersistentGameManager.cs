using System;
using System.Collections;
using OTStudios.DDJ.Runtime.Systems.ScriptableEvents;
using UnityEngine;

namespace OTStudios.DDJ.Runtime.Runtime
{
    public class PersistentGameManager : MonoBehaviour
    {
        [SerializeField]
        private ScriptableEvent sessionStartedEvent;

        private static PersistentGameManager instance;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this.gameObject);
            }
            
            DontDestroyOnLoad(this.gameObject);
        }
        
        public void RaiseSessionStartedEventOnNextFrame()
        {
            // It's important that we wait until next frame to raise this event since SceneManager.LoadScene()
            // does not take effect until the following frame. Otherwise we might get an order of execution problem
            // with our events. We also can't do this from the game over menu since reloading the scene will destroy 
            // all MonoBehaviours and therefore stop any running coroutines. I've opted to create a "persistent game
            // manager" instead of messing with the existing game manager we have set up so as to avoid any conflicts
            // (like with audio, for instance)
            IEnumerator RaiseSessionStartedEventNextFrame()
            {
                // Skip a frame
                yield return null;
                
                this.sessionStartedEvent.Raise();
            }
            
            StartCoroutine(RaiseSessionStartedEventNextFrame());
        }
    }
}