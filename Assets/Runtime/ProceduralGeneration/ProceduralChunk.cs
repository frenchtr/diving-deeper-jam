using UnityEngine;
using Object = UnityEngine.Object;

namespace OTStudios.DDJ.Runtime.Runtime.ProceduralGeneration
{
    public class ProceduralChunk : MonoBehaviour
    {
        [SerializeField]
        private ProceduralChunkLoader chunkLoader;
        
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
            var positionToSpawnAt = new Vector3()
            {
                x = 0f,
                y = player.transform.position.y - 10f,
                z = 0f,
            };
                Object.Instantiate(nextChunkToInstantiate, positionToSpawnAt, Quaternion.identity);
            
            // Despawn this chunk
            Object.Destroy(this.gameObject);
        }
    }
}
