using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private RectTransform winContent;
    [SerializeField] private RectTransform loseContent;
    [SerializeField] private PauseMenu pauseMenu;
    
    private void Start()
    {
        winContent.gameObject.SetActive(false);
        loseContent.gameObject.SetActive(false);
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
        winContent.gameObject.SetActive(true);
        pauseMenu.PauseGame();
    }

    public void ShowLoseMenu()
    {
        loseContent.gameObject.SetActive(true);
        pauseMenu.PauseGame();
    }
}
