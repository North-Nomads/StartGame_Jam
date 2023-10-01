using Player;
using UnityEngine;

namespace WorldGeneration
{
    public abstract class PlatformEffect : MonoBehaviour
    {
        [SerializeField] private GameObject modelToHide;
        public abstract void ExecuteOnPickUp(PlayerMovement player);
        public bool IsPickable { get; set; } = true;

        public void HideChildren()
        {
            modelToHide.SetActive(false);
        }
    }
}