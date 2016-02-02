using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameOver : MonoBehaviour {
    
        void Start ()
        {
            Button button = transform.GetComponent<Button>();
            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = "You displeased the gods! Your final score is " + 
                GameData.GetInstance().GetTotalPointsCollected() + ".\nRetry?";
        }

        public void RestartGame()
        {
            GameData.GetInstance().ResetGame();
            SceneManager.LoadScene(1);
        }
    }
}
