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
            var totalLevels = SceneManager.sceneCountInBuildSettings;

            print("Start");
            // i = 2 because (id)0 = main menu; (id)1 = levels scene
            for (int i = 2; i < totalLevels; i++)
            {
                print(i);
                var button = Instantiate(buttonPrefab, buttonsParent);
                button.Text.text = i.ToString();
                button.LevelID = i;
            }
        }

        public void ExitToMainMenu()
        {
            SceneManager.LoadScene(SceneIDs.MainMenuSceneID);
        }
    }
}