using Player;
using System.Collections.Generic;
using UnityEngine;

namespace WorldGeneration
{
	public class PortalPlatform : WorldPlatform
	{
		private static readonly List<PortalPlatform> levelPortals = new();

        private int _index;

        private void Start()
        {
            _index = levelPortals.Count;
            levelPortals.Add(this);
        }

        public override void OnReach(PlayerMovement player)
        {
            var next = levelPortals[(_index + 1) % levelPortals.Count];
            Vector2Int newPosition = new(next.X, next.Z);
            player.PlayerPlatformX = next.X;
            player.PlayerPlatformZ = next.Z;
            player.transform.position += next.transform.position - transform.position;
            player.PlayerPath.Add(newPosition);
        }

        private void OnDestroy()
        {
            levelPortals.Remove(this);
        }
    }
}