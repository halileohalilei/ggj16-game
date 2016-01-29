using UnityEngine;

namespace Assets.Scripts
{
    public class Collectible : MonoBehaviour
    {

        void OnCollisionEnter()
        {
            Debug.Log("Collided");
            //TODO add points
            Destroy(gameObject);
        }
    }
}
