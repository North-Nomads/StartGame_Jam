using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private RectTransform content;
        private static bool _isPaused;
        public static bool IsPaused => _isPaused;

        private void Start()
        {
            UnpauseGame();
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ExitToMainMenu()
        {
            SceneManager.LoadScene(SceneIDs.MainMenuSceneID);
        }

        public void HandlePausePressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (_isPaused)
                    UnpauseGame();
                else
                    PauseGame(true);
            }
        }

        public void PauseGame(bool withWindow=false)
        {
            _isPaused = true;
            content.gameObject.SetActive(withWindow);
            Time.timeScale = 0;
        }

        public void UnpauseGame()
        {
            _isPaused = false;
            content.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
