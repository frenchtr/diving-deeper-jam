using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OTStudios.DDJ.Runtime.Runtime.Systems;

namespace OTStudios.DDJ.Runtime {

    public class MainMenu : MonoBehaviour {

        [SerializeField] private GameObject mainMenuContent;

        private static bool autoStart;
        private void Awake() {
            mainMenuContent.SetActive(true);
        }

        private void Start() {

            if (autoStart) {
                autoStart = false;
                StartGame();
            }
        }

        public static void Load(bool autoStart = false) {
            MainMenu.autoStart = autoStart;
            SceneManager.ReloadScene();
        }


        public void StartGame() {
            mainMenuContent.SetActive(false);

            GameManager.StartGame();
        }

        public void QuitGame() {

            #if UNITY_WEBGL
            #else
            Application.Quit();
            #endif
        }
    }
}
