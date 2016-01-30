using UnityEngine;
using System;
using Random = System.Random;

namespace Assets.Scripts
{
    class PlaneManager : MonoBehaviour
    {
        private const int SouthEnd = -50;
        private const int NorthEnd = 50;
        private const int EastEnd = 50;
        private const int WestEnd = -50;

        public GameObject ObstaclePrefab = null;
        public GameObject CollectiblePrefab = null;

        [SerializeField]
        private int ObstacleCount = 0;
        [SerializeField]
        private int CollectibleCount = 0;

        void Start()
        {
            GeneratePlane();
        }

        public void GeneratePlane()
        {
            GameLevel currentLevel = GameData.GetInstance().GetCurrentLevel();

            GenerateObstacles(currentLevel);
            GenerateCollectibles(currentLevel);
        }

        private void GenerateObstacles(GameLevel currentLevel)
        {
            var obstacleContainer = GameObject.Find("Obstacles").transform;
            var xPositions = Util.GenerateRandomArray(WestEnd, EastEnd);
            var zPositions = Util.GenerateRandomArray(SouthEnd, NorthEnd);
            for (var i = 0; i < ObstacleCount; i++)
            {
                var randomPosition = new Vector3(xPositions[i], ObstaclePrefab.transform.position.y, zPositions[i]);
                var currentObstacle = (GameObject) Instantiate(ObstaclePrefab, randomPosition, Quaternion.identity);

                if (currentObstacle != null)
                {
                    currentObstacle.transform.parent = obstacleContainer;
                }
            }
        }

        private void GenerateCollectibles(GameLevel currentLevel)
        {
            var collectibleContainer = GameObject.Find("Collectibles").transform;
            var xPositions = Util.GenerateRandomArray(WestEnd, EastEnd);
            var zPositions = Util.GenerateRandomArray(SouthEnd, NorthEnd);
            for (var i = 0; i < CollectibleCount; i++)
            {
                var randomPosition = new Vector3(xPositions[i], CollectiblePrefab.transform.position.y, zPositions[i]);
                var currentCollectible = (GameObject)Instantiate(CollectiblePrefab, randomPosition, Quaternion.identity);

                if (currentCollectible != null)
                {
                    currentCollectible.transform.parent = collectibleContainer;
                }
            }
        }
    }
}
