using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class TempoManager : MonoBehaviour
    {
        private static TempoManager _instance;

        private UIHandler _uiHandler;

        private PlayerController _player;

        private readonly float[] _beatsPerMinute = {102f, 130f, 135f, 111f};

        private readonly string[] _possibleMoves = {"Up", "Down", "Left", "Right"};

        private float _songStartTime = -1f;
        private float _lastTick = -1f;
        private float _nextTick = -1f;

        private string _nextMove = "Up";

        private float _currentBpm = -1f;

        private int _totalTicks = 0;

        private float _tolerance = 0.3f;

        private Random _random = new Random();

        void Start()
        {
            _instance = this;

            _uiHandler = GameObject.Find("Canvas").GetComponent<UIHandler>();
            _player = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        }

        void Update()
        {
            if (_songStartTime > 0f)
            {
                var nextTickAssigned = false;
                if (Input.GetButtonDown(_nextMove))
                {
                    var buttonPressTime = Time.time;
                    if (buttonPressTime > _nextTick - _tolerance &&
                        buttonPressTime < _nextTick + _tolerance)
                    {
                        Debug.Log("Hit!");
                        _player.MoveTo(_nextMove);
                        _lastTick = _nextTick;
                        _nextTick = _lastTick + 60f/_currentBpm;
                        nextTickAssigned = true;
                    }
                    else
                    {
                        Debug.Log("Missed!");
                    }
                }

                if (Time.time > _nextTick + _tolerance)
                {
                    _lastTick = _nextTick;
                    _nextTick = _lastTick + 60f/_currentBpm;
                    nextTickAssigned = true;
                    Debug.Log("Late!");
                }
                if (nextTickAssigned)
                {
                    _nextMove = _possibleMoves[_random.Next(0, 4)];
                    _uiHandler.ToggleIndicator(_nextMove);
                }

            }
        }

        public static TempoManager GetInstance()
        {
            return _instance;
        }
        
        public void StartGame()
        {
            int i = (int) GameData.GetInstance().GetCurrentLevel();
            _currentBpm = _beatsPerMinute[i];
            _songStartTime = Time.time;
            _lastTick = _songStartTime;
            _nextTick = _lastTick + 60f/_currentBpm; // + _offsets[i];
        }
    }
}
