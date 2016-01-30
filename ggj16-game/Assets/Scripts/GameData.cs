using System;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    class GameData : MonoBehaviour
    {
        private UIHandler _uiHandler;

        private GameLevel _currentLevel;
        private int _pointsCollectedThisLevel;
        private int _totalPointsCollected;
        private float _remainingTimeInThisLevel;

        private static GameData _instance;

        [SerializeField] private PlaneManager _planeManager;
        [SerializeField] private PlayerController _player;

        void Start()
        {
            Debug.Log("GameData.Start()");
            _instance = this;

            _uiHandler = GameObject.Find("Canvas").GetComponent<UIHandler>();

            _totalPointsCollected = 0;
            _remainingTimeInThisLevel = 20f;

            ChangeLevel();
            
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

        private void ChangeLevel()
        {
            _totalPointsCollected += _pointsCollectedThisLevel;
            _pointsCollectedThisLevel = 0;

            Array values = Enum.GetValues(typeof(GameLevel));
            Random random = new Random();
            _currentLevel = (GameLevel)values.GetValue(random.Next(values.Length));
        }

        void Update()
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

        private void OnLevelFailed()
        {
            _player.DisableControls();
            _uiHandler.DisableUI();
            _uiHandler.FadeToBlack();
        }
    }
}
