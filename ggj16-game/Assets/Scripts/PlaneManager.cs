using UnityEngine;
using System;
using Random = System.Random;

namespace Assets.Scripts
{
    class PlaneManager : MonoBehaviour
    {
        private const int SouthEnd = -10;
        private const int NorthEnd = 10;
        private const int EastEnd = 10;
        private const int WestEnd = -10;

        [SerializeField] private GameObject ObstaclePrefab = null;
        [SerializeField] private GameObject CollectiblePrefab = null;
        [SerializeField] private GameObject MagicCircle = null;
        [SerializeField] private GameObject Plane = null;

        [SerializeField]
        private int ObstacleCount = 0;
        [SerializeField]
        private int CollectibleCount = 0;
        
        void Start()
        {
//            Debug.Log("PlaneManager.Start()");
            GeneratePlane();
        }

        public void GeneratePlane()
        {
            GameLevel currentLevel = GameData.GetInstance().GetCurrentLevel();

            GenerateObstacles(currentLevel);
            GenerateCollectibles(currentLevel);
            Random random = new Random();
            var randomX = random.NextDouble() * (EastEnd - WestEnd - 2) + WestEnd - 1;
            var randomZ = random.NextDouble() * (NorthEnd - SouthEnd - 2) + SouthEnd - 1;
            MagicCircle.transform.position = new Vector3((float) (randomX/2), 0.1f, (float) (randomZ/2));

            MeshRenderer planeRenderer = Plane.GetComponent<MeshRenderer>();
            switch (currentLevel)
            {
                case GameLevel.Classic:
                {
                    planeRenderer.material.color = Color.green;
                    break;
                }
                case GameLevel.Electronic:
                {
                    planeRenderer.material.color = Color.blue;
                    break;
                }
                case GameLevel.Jazz:
                {
                    planeRenderer.material.color = Color.white;
                    break;
                }
                case GameLevel.Metal:
                {
                    planeRenderer.material.color = new Color(0.25f, 0.25f, 0.25f,1f);
                    break;
                }
            }
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
                    SpriteRenderer obstacleRenderer = currentObstacle.GetComponentInChildren<SpriteRenderer>();
                    obstacleRenderer.sprite = Resources.Load<Sprite>("Sprites/agac_" + (int)currentLevel);
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
                    SpriteRenderer collectibleRenderer = currentCollectible.GetComponentInChildren<SpriteRenderer>();
                    collectibleRenderer.sprite = Resources.Load<Sprite>("Sprites/nota_" + (int)currentLevel);
                    currentCollectible.transform.parent = collectibleContainer;
                }
            }
        }
    }
}
