using Player;

namespace WorldGeneration
{
    public interface IPlatformEffect
    {
        public void ExecuteOnPickUp(PlayerMovement player);
    }
}