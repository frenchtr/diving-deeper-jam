using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTStudios.DDJ.Runtime.Runtime.ProceduralGeneration {

    [RequireComponent(typeof(BoxCollider2D))]
    public class ProceduralChunk : MonoBehaviour {

        [SerializeField] private ProceduralChunkLoader chunkLoader;
        [SerializeField] private float height, offset;

        private BoxCollider2D trigger;

        private void OnValidate() {

            if (trigger == null) trigger = GetComponent<BoxCollider2D>();

            trigger.offset = Vector2.up * offset;
            trigger.size = new Vector2(trigger.size.x, height);
        }

        private const float levelWidth = 16f;

        private void OnDrawGizmos() {
            Gizmos.color = new Color(1, 0, 0, 0.1f);
            Gizmos.DrawCube(transform.position + Vector3.up * offset, new Vector2(levelWidth, height));
        }

        private void OnTriggerEnter2D(Collider2D collision) {

            if (!collision.TryGetComponent(out Player player)) return;

            var newChunk = chunkLoader.GetChunk().GetComponent<ProceduralChunk>();
            Vector2 position = new() {
                x = transform.position.x,
                y = transform.position.y + offset - height / 2f - newChunk.height / 2f - newChunk.offset,
            };

            Instantiate(newChunk.gameObject, position, Quaternion.identity);

            Destroy(this);
        }
    }
}
