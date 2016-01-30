using System;

namespace Assets.Scripts
{
    class GameData
    {
        private static GameData _instance;

        private GameLevel _currentLevel;
        private int _pointsCollectedThisLevel;
        private int _totalPointsCollected;

        private GameData()
        {
            _totalPointsCollected = 0;

            ChangeLevel();
        }

        public static GameData GetInstance()
        {
            if (_instance == null)
                _instance = new GameData();
            return _instance;
        }

        public GameLevel GetCurrentLevel()
        {
            return _currentLevel;
        }

        private void ChangeLevel()
        {
            _totalPointsCollected += _pointsCollectedThisLevel;
            _pointsCollectedThisLevel = 0;

            Array values = Enum.GetValues(typeof(GameLevel));
            Random random = new Random();
            _currentLevel = (GameLevel)values.GetValue(random.Next(values.Length));
        }
    }
}
