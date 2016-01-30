using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIHandler : MonoBehaviour
    {
        private Text _remainingTimeText;
        private Text _pointsCollectedText;
        private Image _fadeOutImage;

        private bool _fadeToBlack; // \m/

        private float _fadeSpeed = 3f;
        
        void Start ()
        {
            _remainingTimeText = transform.FindChild("Time Remaining").GetComponent<Text>();
            _pointsCollectedText = transform.FindChild("Points Collected").GetComponent<Text>();
            _fadeOutImage = transform.FindChild("FadeOut").GetComponent<Image>();
        }

        void Update()
        {
            if (_fadeToBlack)
            {
                _fadeOutImage.color = Color.Lerp(_fadeOutImage.color, Color.black, _fadeSpeed * Time.deltaTime);

                if (_fadeOutImage.color.a > .99f)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }

        public void UpdateRemainingTime(float remainingTimeInThisLevel)
        {
            _remainingTimeText.text = "Remaining Time: " + remainingTimeInThisLevel.ToString("F", CultureInfo.InvariantCulture);
        }

        public void UpdatePointsCollected(int pointsCollected)
        {
            _pointsCollectedText.text = "Points Collected: " + pointsCollected;
        }

        public void DisableUI()
        {
            _remainingTimeText.enabled = false;
            _pointsCollectedText.enabled = false;
        }

        public void FadeToBlack()
        {
            _fadeToBlack = true;
        }
    }
}
