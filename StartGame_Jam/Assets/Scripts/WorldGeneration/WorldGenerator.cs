using Player;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace WorldGeneration
{
    /// <summary>
    /// A start point of the level. Responsible for initialization 
    /// </summary>
    public class WorldGenerator : MonoBehaviour
    {
        // Size of 2d array
        [SerializeField] private int levelSizeX;
        [SerializeField] private int levelSizeZ;
        
        // Prefabs of platforms (index = id)
        [SerializeField] private WorldPlatform[] platformPrefabs;
        // Effects array that are sorted by their id
        [SerializeField] private PlatformEffect[] platformEffects;
        // Player prefab
        [SerializeField] private PlayerMovement playerPrefab;
        // HackerNPC prefab
        [SerializeField] private HackerNPC hacker;

        // Array of platforms 
        private WorldPlatform[,] _worldPlatforms;

        public WorldPlatform this[int x, int z] => _worldPlatforms[x, z];

        /// <summary>
        /// Gets the X-size of the level.
        /// </summary>
        public int LevelSizeX => levelSizeX;

        /// <summary>
        /// Gets the Z-size of the level.
        /// </summary>
        public int LevelSizeZ => levelSizeZ;

        private void Start()
        {
            // Read txt
            LoadLevel(SceneManager.GetActiveScene().name + ".map");
            // Initialize level
            
            // Spawn player
            var player = Instantiate(playerPrefab);
            player.World = this;
            
            int playerStartX = 0;
            int playerStartZ = _worldPlatforms.GetLength(1) / 2;
            
            player.PlayerPlatformX = playerStartX;
            player.PlayerPlatformZ = playerStartZ;
            player.transform.position = _worldPlatforms[playerStartX, playerStartZ].PlayerPivot.position;
            StartCoroutine(SpawnHacker());
            
        }

        private IEnumerator SpawnHacker()
        {
            yield return new WaitForSeconds(5);
            var hackernpc = Instantiate(hacker);
            hackernpc.World = this;
            hackernpc.transform.position = _worldPlatforms[0, _worldPlatforms.GetLength(1) / 2].PlayerPivot.position;
        }

        /// <summary>
        /// Checks if platform on (x, z) is reachable.
        /// </summary>
        /// <param name="x">The X coordinate in the 2D array</param>
        /// <param name="z">The Z coordinate in the 2D array</param>
        /// <returns><see langword="true"/> if the platform is reachable.</returns>
        public bool AreCoordinatesReachable(int x, int z)
        {
            if (x >= levelSizeX || x < 0)
                return false;
            
            if (z >= levelSizeZ || z < 0)
                return false;
                
            return _worldPlatforms[x, z].IsReachable;
        }

        /// <summary>
        /// Loads the level from its binary file.
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadLevel(string filePath)
        {
            using Stream stream = File.OpenRead(Path.Combine(Application.dataPath, "Resources", "Levels", filePath));
            using BinaryReader reader = new(stream);
            levelSizeX = reader.ReadByte();
            levelSizeZ = reader.ReadByte();
            _worldPlatforms = new WorldPlatform[levelSizeX, levelSizeZ];
            for (int i = 0; i < levelSizeX; i++)
            {
                for (int j = 0; j < levelSizeZ; j++)
                {
                    byte id = reader.ReadByte();
                    var tile = Instantiate(platformPrefabs[id]);
                    tile.transform.position = new Vector3(i, 0, j);
                    tile.X = i;
                    tile.Z = j;
                    _worldPlatforms[i, j] = tile;
                }
            }
            for (int i = 0; i < levelSizeX; i++)
            {
                for (int j = 0; j < levelSizeZ; j++)
                {
                    byte id = reader.ReadByte();
                    if (id != 0)
                    {
                        var effect = platformEffects[id];
                        _worldPlatforms[i, j].Effect = effect;
                    }
                }
            }
        }

        public PlatformEffect GetPlatformEffect(int x, int z)
        {
            return _worldPlatforms[x, z].Effect;
        }
    }
}
