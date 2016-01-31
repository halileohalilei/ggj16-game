using UnityEngine;

namespace Assets.Scripts
{
    class RitualHandler : MonoBehaviour
    {
        private static RitualHandler _instance;

        [SerializeField] private GameObject _jazzGodPrefab;
        [SerializeField] private GameObject _electronicGodPrefab;
        [SerializeField] private GameObject _metalGodPrefab;
        [SerializeField] private GameObject _classicalGodPrefab;

        [SerializeField] private Transform _magicCircle;

        void Start()
        {
            _instance = this;
        }

        public static RitualHandler GetInstance()
        {
            return _instance;
        }

        public void SpawnGod()
        {
            
        }

    }
}
