using System.Collections;
using Level;
using Player;
using UnityEngine;
using WorldGeneration;

namespace Effects
{
    public class CoinEffect : PlatformEffect
    {
        public override void ExecuteOnPickUp(PlayerMovement player)
        {
            LevelJudge.Coins++;
        }
    }
}