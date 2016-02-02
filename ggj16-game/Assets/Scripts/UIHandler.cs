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

        private Transform _moveIndicators;

        private AudioSource _musicSource;

        private Text _currentIndicator;

        private int _fading;

        private float _fadeSpeed = 3f;

        private float _countdownTimer = -1f;
        private bool _countdownActive;

        private static UIHandler _instance;

        public static UIHandler GetInstance()
        {
            return _instance;
        }

        void Start ()
        {
            _instance = this;

            _remainingTimeText = transform.FindChild("Time Remaining").GetComponent<Text>();
            _pointsCollectedText = transform.FindChild("Points Collected").GetComponent<Text>();
            _countdownText = transform.FindChild("Countdown").GetComponent<Text>();
            _fadeOutImage = transform.FindChild("FadeOut").GetComponent<Image>();

            _countdownText.enabled = false;

            _moveIndicators = transform.FindChild("MoveIndicators");
            _moveIndicators.FindChild("Left").GetComponent<Text>().enabled = false;
            _moveIndicators.FindChild("Right").GetComponent<Text>().enabled = false;
            _moveIndicators.FindChild("Up").GetComponent<Text>().enabled = false;
            _moveIndicators.FindChild("Down").GetComponent<Text>().enabled = false;

            _moveIndicators.gameObject.SetActive(false);
        }

        void Update()
        {
            if (_fading == -1)
            {
                _fadeOutImage.color = Color.Lerp(_fadeOutImage.color, Color.black, _fadeSpeed*Time.deltaTime);
                if (_fadeOutImage.color.a > .99f)
                {
                    SceneManager.LoadScene(2);
                }
            }
            else if (_fading == 1)
            {
                _fadeOutImage.color = Color.Lerp(_fadeOutImage.color, Color.white, _fadeSpeed * Time.deltaTime);
                if (_fadeOutImage.color.a > .99f)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
                        GameData.GetInstance().StartSecondPhase();
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
            _fading = -1;
            AudioManager.GetInstance().FadeOutSong();
        }

        public void FadeToWhite()
        {
            _fading = 1;
            Color transparentWhite = Color.white;
            transparentWhite.a = 0f;
            _fadeOutImage.color = transparentWhite;
            AudioManager.GetInstance().FadeOutSong();
        }

        public void StartCountdown()
        {
            _countdownText.enabled = true;
            _countdownTimer = 5f;
            _countdownActive = true;
        }

        public void ToggleIndicator(string indicatorName)
        {
            if (!_moveIndicators.gameObject.activeSelf)
            {
                _moveIndicators.gameObject.SetActive(true);
            }
            if (_currentIndicator != null)
            {
                _currentIndicator.enabled = false;
            }
            
            var indicator = _moveIndicators.FindChild(indicatorName).GetComponent<Text>();
            indicator.enabled = true;
            _currentIndicator = indicator;
        }
    }
}
