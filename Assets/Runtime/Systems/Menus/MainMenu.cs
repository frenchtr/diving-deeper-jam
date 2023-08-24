using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTStudios.DDJ.Runtime {

    public class MainMenu : MonoBehaviour {

        [SerializeField] private GameObject mainMenuContent;

        public void StartGame() {
            mainMenuContent.SetActive(false);
        }

        public void QuitGame() {

            #if UNITY_WEBGL
            #else
            Application.Quit();
            #endif
        }
    }
}
