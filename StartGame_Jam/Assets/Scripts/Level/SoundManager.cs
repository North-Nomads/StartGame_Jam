using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class SoundManager : MonoBehaviour
    {
        private static bool isSoundEnabled = true;

        [SerializeField] private AudioSource audioSource;

        public static bool IsSoundEnabled => isSoundEnabled;

        private void Start()
        {
            audioSource.mute = !isSoundEnabled;
        }

        public void ToggleMute()
        {
            isSoundEnabled = !isSoundEnabled;
            audioSource.mute = !isSoundEnabled;
        }
    }
}