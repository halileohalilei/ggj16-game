namespace Assets.Scripts
{
    class GameData
    {
        private static GameData _instance;

        private GameLevel _currentLevel;
        private int _pointsCollectedThisLevel;

        public enum GameLevel
        {
            Jazz, Metal, Classic, Electronic
        }

        private GameData()
        {
            _pointsCollectedThisLevel = 0;
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
    }
}
