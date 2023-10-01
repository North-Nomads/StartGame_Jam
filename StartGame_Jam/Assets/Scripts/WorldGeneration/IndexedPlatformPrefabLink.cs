using System;

namespace WorldGeneration
{
	[Serializable]
	public class IndexedPlatformPrefabLink
	{
		public int Index;

		public WorldPlatform Prefab;
	}

	public class IndexedPlatformEffectLink
	{
		public int Index;

		public PlatformEffect EffectPrefab;
	}
}