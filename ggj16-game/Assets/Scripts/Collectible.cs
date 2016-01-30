using UnityEngine;

namespace Assets.Scripts
{
    public class Collectible : MonoBehaviour
    {

        void OnCollisionEnter()
        {
            //TODO add points
            Destroy(gameObject);
        }
    }
}
