using Player;
using System;
using UnityEngine;

namespace WorldGeneration
{
    public abstract class PlatformEffect : ScriptableObject
    {
        public abstract void OnPlayerGetInto(PlayerMovement player);
    }
}