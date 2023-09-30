using Player;
using WorldGeneration;

namespace Effects
{
    public class MusicEffect : PlatformEffect
    {
        public override void ExecuteOnPickUp(PlayerMovement player)
        {
            player.HasBarrier = true;
        }
    }
}