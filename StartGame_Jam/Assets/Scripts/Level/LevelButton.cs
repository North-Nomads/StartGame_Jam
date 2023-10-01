using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        private const int LevelScene = 2;
        public int LevelID { get; set; }
        public TextMeshProUGUI Text => text;

        public void LoadLinkedLevel()
        {
            SceneIDs.LoadedLevelID = LevelID;
            SceneManager.LoadScene(LevelScene);
        }
        
    }
}