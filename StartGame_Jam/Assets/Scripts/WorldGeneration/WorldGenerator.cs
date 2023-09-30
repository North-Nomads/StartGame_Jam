using System;
using UnityEngine;

namespace WorldGeneration
{
    /// <summary>
    /// A start point of the level. Responsible for initialization 
    /// </summary>
    public class WorldGenerator : MonoBehaviour
    {
        // Size of 2d array
        [SerializeField] private int levelSizeZ;
        [SerializeField] private int levelSizeX;
        
        // Prefabs of platforms (index = id)
        [SerializeField] private WorldPlatform[] platformPrefabs;
        // Effects array that are sorted by their id
        [SerializeField] private PlatformEffect[] platformEffects;
        
        // Array of platforms 
        private WorldPlatform[,] _worldPlatforms;

        private void Start()
        {
            // Read txt
            // Initialize level
            // Spawn player
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if platform on (x, z)
        /// </summary>
        /// <param name="x">the X coordinate in 2D array</param>
        /// <param name="z">the Z coordinate in 2D array</param>
        /// <returns>True if target platform isReachable == true</returns>
        public bool AreCoordinatesReachable(int x, int z)
        {
            if (x >= levelSizeX || x < 0)
                return false;
            
            if (z >= levelSizeZ || z < 0)
                return false;
                
            return _worldPlatforms[x, z].IsReachable;
        }

        public PlatformEffect GetPlatformEffect(int x, int z)
        {
            return _worldPlatforms[x, z].Effect;
        }
    }
}
