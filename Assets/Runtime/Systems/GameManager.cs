using System;
using System.Collections;
using OTStudios.DDJ.Runtime.Runtime.Bricks;
using OTStudios.DDJ.Runtime.Systems.ScriptableEvents;
using UnityEngine;

namespace OTStudios.DDJ.Runtime.Runtime.Systems
{
    public class GameManager : MonoBehaviour
    {
        [Header("Registries")]
        [SerializeField]
        private BrickRegistry brickRegistry;
        [Header("Events")]
        [SerializeField]
        private ScriptableEvent gameStartedEvent;

        [Header("Music")]
        [SerializeField]
        private AudioSource musicSource;
        [SerializeField]
        private AudioClip musicIntro, musicMainLoop;

        private static GameManager I;

        private void Awake()
        {
            this.brickRegistry.Setup();
            I = this;
        }

        private void OnDestroy()
        {
            this.brickRegistry.Teardown();
        }

        private void Start()
        {
            this.gameStartedEvent.Raise();
        }

        public static void StartGame() => I.StartGameInternal();
        private void StartGameInternal() {

            StartCoroutine(IntroMusic());
            IEnumerator IntroMusic() {

                musicSource.clip = musicIntro;
                musicSource.loop = false;
                musicSource.Play();

                yield return new WaitForSeconds(musicIntro.length);

                musicSource.clip = musicMainLoop;
                musicSource.loop = true;
                musicSource.Play();
            }
        }
    }
}
