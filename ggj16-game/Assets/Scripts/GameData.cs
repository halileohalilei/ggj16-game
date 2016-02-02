using System;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    class GameData : MonoBehaviour
    {
        private UIHandler _uiHandler;

        private static GameLevel _currentLevel = GameLevel.Electronic;
        private static float _remainingTimeInThisLevel = 30f;
        private static int _totalPointsCollected;

        private int _pointsCollectedThisLevel;

        private bool _firstPhaseEnded;
        private bool _startSecondPhase;

        private static GameData _instance;

        [SerializeField] private PlaneManager _planeManager;
        [SerializeField] private PlayerController _player;
        [SerializeField] private GameObject _plane;
        [SerializeField] private GameObject _obstacles;
        [SerializeField] private GameObject _collectibles;

        private Vector3 _cameraTargetPos;
        private float _cameraSpeed = 2f;
        private bool _isCameraMoving;

        void Start()
        {
            Debug.Log("GameData.Start()");
            _instance = this;

            _uiHandler = GameObject.Find("Canvas").GetComponent<UIHandler>();
            _uiHandler.UpdatePointsCollected(_totalPointsCollected);
            
            _planeManager.gameObject.SetActive(true);
        }

        public GameLevel GetCurrentLevel()
        {
            return _currentLevel;
        }

        public static GameData GetInstance()
        {
            return _instance;
        }

        public void IncrementPointsCollected()
        {
            _totalPointsCollected++;
            _uiHandler.UpdatePointsCollected(_totalPointsCollected);
        }

        private GameLevel ChangeLevel()
        {
            _totalPointsCollected += _pointsCollectedThisLevel;
            _pointsCollectedThisLevel = 0;

            Array values = Enum.GetValues(typeof(GameLevel));
            Random random = new Random();
            _currentLevel = (GameLevel)values.GetValue(random.Next(values.Length));
            return _currentLevel;
        }

        void Update()
        {
            if (!_firstPhaseEnded)
            {
                _remainingTimeInThisLevel -= Time.deltaTime;
                if (_remainingTimeInThisLevel <= 0f)
                {
                    OnLevelFailed();
                }
                else
                {
                    _uiHandler.UpdateRemainingTime(_remainingTimeInThisLevel);
                }
            }
            else
            {
                if (_isCameraMoving)
                {
                    Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
                        _cameraTargetPos, _cameraSpeed * Time.deltaTime);
                    if (Vector3.Magnitude(_cameraTargetPos - Camera.main.transform.position) < 0.01f)
                    {
                        _isCameraMoving = false;
                        _startSecondPhase = true;
                    }
                }
                else
                {
                    if (_startSecondPhase)
                    {
                        Camera.main.transform.parent = null;
                        _uiHandler.StartCountdown();
                        _startSecondPhase = false;
                    }
                }
            }
        }

        private void OnLevelFailed()
        {
            EndFirstPhase();
            _uiHandler.FadeToBlack();
        }

        private void EndFirstPhase()
        {
            _player.DisableControls();
            _uiHandler.DisableUI();
            _firstPhaseEnded = true;
        }

        public void SwitchToSecondPhase()
        {
            EndFirstPhase();
            _plane.SetActive(false);
            _obstacles.SetActive(false);
            _collectibles.SetActive(false);
            _player.GetComponent<Rigidbody>().isKinematic = true;

            Transform magicCircle = GameObject.Find("Magic Circle").transform;
            Transform camTransform = Camera.main.transform;
            _cameraTargetPos = camTransform.position;
            _cameraTargetPos = magicCircle.position;
            _cameraTargetPos = _cameraTargetPos - camTransform.forward*6f;

            _isCameraMoving = true;

            AudioManager.GetInstance().FadeOutSong();
        }

        public void StartSecondPhase()
        {
            AudioManager.GetInstance().StartSong(ChangeLevel());
            TempoManager.GetInstance().StartGame();
            _planeManager.SpawnGod(_currentLevel);
        }

        public void SetRemainingTime(float remainingTime)
        {
            _remainingTimeInThisLevel = remainingTime;
        }

        public void ResetGame()
        {
            _totalPointsCollected = 0;
            _remainingTimeInThisLevel = 30f;
            _currentLevel = GameLevel.Electronic;
        }

        public int GetTotalPointsCollected()
        {
            return _totalPointsCollected;
        }
    }
}
