using UnityEngine;

namespace Assets.Scripts
{
    class PlaneManager : MonoBehaviour
    {
        private const float SouthEnd = -50f;
        private const float NorthEnd = 50f;
        private const float EastEnd = 50f;
        private const float WestEnd = -50f;

        public GameObject ObstaclePrefab;
        public GameObject CollectiblePrefab;

        public void GeneratePlane()
        {
            GameLevel currentLevel = GameData.GetInstance().GetCurrentLevel();

            GenerateObstacles(currentLevel);
            GenerateCollectibles(currentLevel);
        }

        private void GenerateObstacles(GameLevel currentLevel)
        {
            //TODO generate obstacles
        }

        private void GenerateCollectibles(GameLevel currentLevel)
        {
            //TODO generate collectibles (lol)
        }
    }
}
