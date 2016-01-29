using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {

        private Direction _direction;
        [SerializeField] private Animator _animator;

        void Start ()
        {
            _animator = GetComponent<Animator>();
        }
	
        void FixedUpdate ()
        {
            if (UpdateInput())
            {
                _animator.SetInteger("Direction", (int) _direction);
            }
        }

        bool UpdateInput()
        {
            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");

            if (vertical > 0)
            {
                _direction = Direction.North;
                Debug.Log("Turned north.");
            }
            else if (vertical < 0)
            {
                _direction = Direction.South;
                Debug.Log("Turned south.");
            }
            else if (horizontal > 0)
            {
                _direction = Direction.East;
                Debug.Log("Turned east.");
            }
            else if (horizontal < 0)
            {
                _direction = Direction.West;
                Debug.Log("Turned west.");
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
