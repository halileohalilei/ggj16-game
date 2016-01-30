using UnityEngine;

namespace Assets.Scripts
{
    public class MagicCircle : MonoBehaviour
    {

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                GameData.GetInstance().SwitchToSecondPhase();
            }
        }
    }
}
