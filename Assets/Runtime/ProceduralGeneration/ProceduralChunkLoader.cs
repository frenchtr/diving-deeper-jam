using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

namespace OTStudios.DDJ.Runtime.Runtime.ProceduralGeneration
{
    [CreateAssetMenu(menuName = "Scriptables/Systems/Procedural Chunk Loader")]
    public class ProceduralChunkLoader : ScriptableObject
    {

        [SerializeField]
        private int maxRepetitionsPerSet;
        [SerializeField]
        private ChunkData[] proceduralChunks;

        [Serializable]
        private struct ChunkData
        {
            public ProceduralChunk prefab;
            //public int difficulty;
        }

        private readonly List<ChunkData> chunkBag = new();

        private void PopulateBag()
        {
            for (int i = 0; i < maxRepetitionsPerSet; i++)
                chunkBag.AddRange(proceduralChunks);

            //bag.Sort((chunk1, chunk2) => chunk1.difficulty - chunk2.difficulty);
        }

        public ProceduralChunk GetChunk()
        {
            if (chunkBag.Count == 0) PopulateBag();

            var randomIndex = Random.Range(0, chunkBag.Count);
            var chunk = chunkBag[randomIndex];
            chunkBag.RemoveAt(randomIndex);

            return chunk.prefab;
        }
    }
}
