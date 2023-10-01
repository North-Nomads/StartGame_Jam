using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private PauseMenu pauseMenu;
    
    private void Start()
    {
        content.gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(SceneIDs.MainMenuSceneID);
    }

    public void LoadNextLevel()
    {
        // TODO: rewrite to get two digits scene number
        var totalLevels = SceneManager.sceneCount;
        var thisLevelChar = SceneManager.GetActiveScene().name[^1];
        var thisLevelNumber = thisLevelChar - '0';
        if (thisLevelChar >= totalLevels)
        {
            ExitToMainMenu();
            return;
        }

        SceneManager.LoadScene(thisLevelNumber + 1);
    }

    public void ShowWinMenu()
    {
        content.gameObject.SetActive(true);
        pauseMenu.PauseGame();
    }
}
