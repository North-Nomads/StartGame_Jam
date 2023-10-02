using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class MainMenu : MonoBehaviour
    {
        public void ExitGame()
        {
            Application.Quit();
        }

        public void LoadLevelsScene()
        {
            SceneManager.LoadScene(SceneIDs.LevelsSceneID);
        }

        public void PlayFirstLevel()
        {
            SceneIDs.TotalLevels = 5;
            SceneIDs.LoadedLevelID = 1;
            SceneManager.LoadScene(SceneIDs.LevelsSceneID + 1);
        }
    }
}