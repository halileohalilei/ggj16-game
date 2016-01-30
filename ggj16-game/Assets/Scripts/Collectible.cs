using UnityEngine;

namespace Assets.Scripts
{
    public class Collectible : MonoBehaviour
    {
        void OnCollisionEnter()
        {
            GameData.GetInstance().IncrementPointsCollected();
            Destroy(gameObject);
        }
    }
}
