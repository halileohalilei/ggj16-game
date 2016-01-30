using UnityEngine;

namespace Assets.Scripts
{
    public class MagicCircle : MonoBehaviour
    {

        void OnTriggerEnter(Collider other)
        {
//            Debug.Log(other.transform.tag);
            if (other.gameObject.tag.Equals("Player"))
            {
                Debug.Log("TRIGGERED!!!");
            }
        }
    }
}
