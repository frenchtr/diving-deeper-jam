using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTStudios.DDJ.Runtime {

    public class GameOverMenu : MonoBehaviour {

        [SerializeField] private GameObject menuContent;

        private void Awake() {
            menuContent.SetActive(false);
        }

        public void Show() {
            menuContent.SetActive(true);
        }

        public void Restart() {
            MainMenu.Load(autoStart: true);
        }

        public void ReturnToMainMenu() {
            MainMenu.Load();
        }
    }
}
