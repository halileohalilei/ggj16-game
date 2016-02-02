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
        
        [SerializeField]
        private GameObject _jazzGodPrefab;
        [SerializeField]
        private GameObject _electronicGodPrefab;
        [SerializeField]
        private GameObject _metalGodPrefab;
        [SerializeField]
        private GameObject _classicalGodPrefab;
        [SerializeField]
        private GameObject[] _rockPrefabs;

        private Transform _obstacleContainer;

        [SerializeField] private GameObject ObstaclePrefab = null;
        [SerializeField] private GameObject CollectiblePrefab = null;
        [SerializeField] private GameObject MagicCircle = null;
        [SerializeField] private GameObject Plane = null;

        [SerializeField]
        private int ObstacleCount = 0;
        [SerializeField]
        private int CollectibleCount = 0;

        private readonly Random _random = new Random();


        void Start()
        {
            GeneratePlane();
        }

        public void GeneratePlane()
        {
            GameLevel currentLevel = GameData.GetInstance().GetCurrentLevel();


            _obstacleContainer = GameObject.Find("Obstacles").transform;

            GenerateObstacles(currentLevel);
            GenerateCollectibles(currentLevel);
            GenerateBorderObstacles(currentLevel);
            var randomX = _random.NextDouble() * (EastEnd - WestEnd - 2) + WestEnd - 1;
            var randomZ = _random.NextDouble() * (NorthEnd - SouthEnd - 2) + SouthEnd - 1;
            MagicCircle.transform.position = new Vector3((float) (randomX/2), 0.1f, (float) (randomZ/2));
            MagicCircle.GetComponent<SpriteRenderer>().sprite = 
                Resources.Load<Sprite>("Sprites/magic_circles/magicli" + _random.Next(1, 5));

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

        private void GenerateBorderObstacles(GameLevel currentLevel)
        {
            float numObstacles = 50;
            for (float i = 0; i < numObstacles; i++)
            {
                var randomDisplacement = new Vector3((float)_random.NextDouble() - .5f, 0f, 0f);
                var position = new Vector3(WestEnd -.5f, 0f, Mathf.Lerp(SouthEnd - .5f, NorthEnd + .5f, i / numObstacles));
                SpawnBorderObstacle(position + randomDisplacement, currentLevel);
                randomDisplacement = new Vector3((float)_random.NextDouble() - .5f, 0f, 0f);
                position = new Vector3(EastEnd + .5f, 0f, Mathf.Lerp(SouthEnd - .5f, NorthEnd + .5f, i / numObstacles));
                SpawnBorderObstacle(position + randomDisplacement, currentLevel);
                randomDisplacement = new Vector3(0f, 0f, (float)_random.NextDouble() - .5f);
                position = new Vector3(Mathf.Lerp(WestEnd - .5f, EastEnd + .5f, i / numObstacles), 0f, SouthEnd - .5f);
                SpawnBorderObstacle(position + randomDisplacement, currentLevel);
                randomDisplacement = new Vector3(0f, 0f, (float)_random.NextDouble() - .5f);
                position = new Vector3(Mathf.Lerp(WestEnd - .5f, EastEnd + .5f, i / numObstacles), 0f, NorthEnd + .5f);
                SpawnBorderObstacle(position + randomDisplacement, currentLevel);
            }
        }

        private void SpawnBorderObstacle(Vector3 position, GameLevel currentLevel)
        {
            string[] rockTypes = { "high", "mid", "low" };
            int[] numRockTypes = { 5, 4, 6 };
            
            var obstacleType = _random.Next(0, 4);
            GameObject obstacle;
            if (obstacleType == 0)
            {
                obstacle = (GameObject)Instantiate(ObstaclePrefab, position, Quaternion.identity);
            }
            else
            {
                obstacle = (GameObject)Instantiate(_rockPrefabs[obstacleType-1], position, Quaternion.identity);
            }

            if (obstacle != null)
            {
                SpriteRenderer obstacleRenderer = obstacle.GetComponentInChildren<SpriteRenderer>();
                if (obstacleType == 0)
                {
                    obstacleRenderer.sprite = Resources.Load<Sprite>("Sprites/agac_" + (int)currentLevel);
                }
                else
                {
                    obstacleRenderer.sprite =
                        Resources.Load<Sprite>("Sprites/taslar/rock_" + rockTypes[obstacleType-1] + "_" + _random.Next(1, numRockTypes[obstacleType-1] + 1));
                    
                }
                obstacle.transform.parent = _obstacleContainer;
            }
        }

        private void GenerateObstacles(GameLevel currentLevel)
        {
            var obstacleContainer = GameObject.Find("Obstacles").transform;
            var xPositions = Util.GenerateRandomArray(WestEnd, EastEnd);
            var zPositions = Util.GenerateRandomArray(SouthEnd, NorthEnd);
            for (var i = 0; i < ObstacleCount; i++)
            {
                var randomPosition = new Vector3(xPositions[i], 0, zPositions[i]);
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
                    collectibleRenderer.sprite = Resources.Load<Sprite>("Sprites/musical_notes/nota_k_" + (int)currentLevel);
                    currentCollectible.transform.parent = collectibleContainer;
                }
            }
        }

        public void SpawnGod(GameLevel level)
        {
            Vector3 spawnPosition = MagicCircle.transform.position + new Vector3(5f, 0f, 0f);
            switch (level)
            {
                case GameLevel.Classic:
                    Instantiate(_classicalGodPrefab, spawnPosition, Quaternion.identity);
                    break;
                case GameLevel.Jazz:
                    Instantiate(_jazzGodPrefab, spawnPosition, Quaternion.identity);
                    break;
                case GameLevel.Electronic:
                    Instantiate(_electronicGodPrefab, spawnPosition, Quaternion.identity);
                    break;
                case GameLevel.Metal:
                    Instantiate(_metalGodPrefab, spawnPosition, Quaternion.identity);
                    break;
            }
        }
    }
}
