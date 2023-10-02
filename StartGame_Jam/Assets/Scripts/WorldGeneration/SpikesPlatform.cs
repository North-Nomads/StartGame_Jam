using Player;

namespace WorldGeneration
{
    public class SpikesPlatform : WorldPlatform
    {
        public override void OnReach(PlayerMovement player)
        {
            if (player.Hacker != null)
            {
                player.Hacker.KillPlayer();
            }
        }
    }
}