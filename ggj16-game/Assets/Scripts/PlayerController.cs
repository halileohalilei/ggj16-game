using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {

        private Direction _direction;
        [SerializeField] private Animator _animator;

        void Start () {
	        _direction = Direction.Down;
        }
	
        void Update () {
	
        }
    }
}
