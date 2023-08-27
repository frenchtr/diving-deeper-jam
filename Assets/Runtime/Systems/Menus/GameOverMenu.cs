using System.Collections;
using System.Collections.Generic;
using OTStudios.DDJ.Runtime.Systems.ScriptableEvents;
using UnityEngine;

namespace OTStudios.DDJ.Runtime {

    public class GameOverMenu : MonoBehaviour {

        [SerializeField] private GameObject menuContent;

        [Header("Events")]
        [SerializeField]
        private ScriptableEvent sessionRestartedEvent;

        private void Awake() {
            menuContent.SetActive(false);
        }

        public void Show() {
            menuContent.SetActive(true);
        }

        public void Restart() {
            MainMenu.Load(autoStart: true);
            this.sessionRestartedEvent.Raise();
        }

        public void ReturnToMainMenu() {
            MainMenu.Load();
        }
    }
}
