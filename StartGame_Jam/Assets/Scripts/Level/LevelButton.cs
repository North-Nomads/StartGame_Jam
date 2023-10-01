using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        public int LevelID { get; set; }
        public TextMeshProUGUI Text => text;

        public void LoadLinkedLevel()
        {
            SceneManager.LoadScene(LevelID);
        }
        
    }
}