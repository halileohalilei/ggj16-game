using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIHandler : MonoBehaviour
    {
        private Text _remainingTimeText;
        private Text _pointsCollectedText;
        
        void Start ()
        {
            _remainingTimeText = transform.FindChild("Time Remaining").GetComponent<Text>();
            _pointsCollectedText = transform.FindChild("Points Collected").GetComponent<Text>();
        }

        public void UpdateRemainingTime(float remainingTimeInThisLevel)
        {
            _remainingTimeText.text = "Remaining Time: " + remainingTimeInThisLevel.ToString("F", CultureInfo.InvariantCulture);
        }

        public void UpdatePointsCollected(int pointsCollected)
        {
            _pointsCollectedText.text = "Points Collected: " + pointsCollected;
        }
    }
}
