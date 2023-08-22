using UnityEditor;
using UnityEngine;

namespace OTStudios.DDJ.Runtime.Runtime.ProceduralGeneration
{
    [CreateAssetMenu(menuName = "Scriptables/Systems/Procedural Chunk Loader")]
    public class ProceduralChunkLoader : ScriptableObject
    {
        [SerializeField]
        private GameObject[] proceduralChunks;

        public GameObject GetChunk()
        {
            var randomIndex = Random.Range(0, this.proceduralChunks.Length);
            var chunk = this.proceduralChunks[randomIndex];

            return chunk;
        }
    }
}