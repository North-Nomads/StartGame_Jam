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
        if (SceneIDs.TotalLevels == SceneIDs.LoadedLevelID)
        {
            ExitToMainMenu();
            return;
        }

        SceneIDs.LoadedLevelID++;
        // Call WorldGenerator scene rebuild
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
