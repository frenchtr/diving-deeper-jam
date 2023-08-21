using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTStudios.DDJ.Runtime {

    public class CameraMovement : MonoBehaviour {

        [SerializeField] private float
            cameraSpeed,
            maxHeightAbovePlayer;

        private Transform player;
        private float height, velocity;

        private void Awake() {
            player = FindObjectOfType<Player>().transform;
        }

        private void Update() {

            Vector3 pos = transform.position;

            height = Mathf.Min(height, player.position.y + maxHeightAbovePlayer);
            pos.y = Mathf.SmoothDamp(pos.y, height, ref velocity, cameraSpeed);

            transform.position = pos;
        }
    }
}
