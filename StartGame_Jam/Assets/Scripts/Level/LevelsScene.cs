using System.IO;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelsScene : MonoBehaviour
    {
        [SerializeField] private LevelButton buttonPrefab;
        [SerializeField] private RectTransform buttonsParent;

        private void Start()
        {
            //var totalLevels = SceneManager.sceneCountInBuildSettings;

            SceneIDs.TotalLevels = 5;
            for (int i = 0; i < SceneIDs.TotalLevels; i++)
            {
                var button = Instantiate(buttonPrefab, buttonsParent);
                button.Text.text = (i+1).ToString();
                button.LevelID = i + 1;
            }
        }

        public void ExitToMainMenu()
        {
            SceneManager.LoadScene(SceneIDs.MainMenuSceneID);
        }
    }
}