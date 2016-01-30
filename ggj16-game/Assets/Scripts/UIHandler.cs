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
        private Text _countdownText;
        private Image _fadeOutImage;

        private bool _fadeToBlack; // \m/

        private float _fadeSpeed = 3f;

        private float _countdownTimer = -1f;
        private bool _countdownActive = false;
        
        void Start ()
        {
            _remainingTimeText = transform.FindChild("Time Remaining").GetComponent<Text>();
            _pointsCollectedText = transform.FindChild("Points Collected").GetComponent<Text>();
            _countdownText = transform.FindChild("Countdown").GetComponent<Text>();
            _fadeOutImage = transform.FindChild("FadeOut").GetComponent<Image>();

            _countdownText.enabled = false;
        }

        void Update()
        {
            if (_fadeToBlack)
            {
                _fadeOutImage.color = Color.Lerp(_fadeOutImage.color, Color.black, _fadeSpeed*Time.deltaTime);

                if (_fadeOutImage.color.a > .99f)
                {
                    SceneManager.LoadScene(1);
                }
            }
            else
            {
                if (_countdownActive)
                {
                    _countdownTimer -= Time.deltaTime;
                    if (_countdownTimer < 3f && _countdownTimer > 2f)
                    {
                        _countdownText.text = "3";
                    }
                    else if (_countdownTimer < 2f && _countdownTimer > 1f)
                    {
                        _countdownText.text = "2";
                    }
                    else if (_countdownTimer < 1f && _countdownTimer > 0f)
                    {
                        _countdownText.text = "1";
                    }
                    else if (_countdownTimer < 0f)
                    {
                        _countdownActive = false;
                        _countdownText.enabled = false;
                    }
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

        public void StartCountdown()
        {
            _countdownText.enabled = true;
            _countdownTimer = 5f;
            _countdownActive = true;
        }
    }
}
