using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{

    class Game : MonoBehaviour
    {
        public Text RemainingTimeText;

        private float _remainingTimeInThisLevel;

        void Start()
        {
            _remainingTimeInThisLevel = 20f;
        }

        void Update()
        {
            _remainingTimeInThisLevel -= Time.deltaTime;
            if (_remainingTimeInThisLevel <= 0f)
            {
                
            }
            else
            {
                RemainingTimeText.text = "Remaining Time: " + _remainingTimeInThisLevel.ToString("F", CultureInfo.InvariantCulture);
            }
        }
    }
}
