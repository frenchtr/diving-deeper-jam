using UnityEngine;
using Object = UnityEngine.Object;

namespace OTStudios.DDJ.Runtime.Runtime.ProceduralGeneration
{
    public class ProceduralChunk : MonoBehaviour
    {
        [SerializeField]
        private ProceduralChunkLoader chunkLoader;
        [SerializeField]
        private float yOffset = 40f;
        
        private void OnTriggerExit2D(Collider2D other)
        {
            // If we triggered on something other than the player, we want to return
            var player = other.gameObject.GetComponent<Player>();
            
            if (player == null)
            {
                return;
            }
            
            // Spawn a new chunk
            var nextChunkToInstantiate = this.chunkLoader.GetChunk();
            var thisPosition = this.transform.position;
            var positionToSpawnAt = new Vector3()
            {
                x = thisPosition.x,
                y = thisPosition.y - this.yOffset,
                z = thisPosition.z,
            };
            Object.Instantiate(nextChunkToInstantiate, positionToSpawnAt, Quaternion.identity);
        }
    }
}
