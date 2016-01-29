using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {

        private Direction _direction;
        private Animator _animator;
        private Rigidbody _rigidbody;

        [SerializeField] private float _maxSpeed;

        void Start ()
        {
            var spriteRenderer = transform.FindChild("Renderer");
            _animator = spriteRenderer.GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();

            spriteRenderer.rotation = Camera.main.transform.rotation;

            _maxSpeed = 2.5f;

//            Camera.main.transform.rotation;
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
            var velocity = _rigidbody.velocity;
            velocity += new Vector3(horizontal, 
                                    0f, 
                                    vertical);
            
            velocity = Vector3.ClampMagnitude(new Vector3(velocity.x, 0f, velocity.z), _maxSpeed);
            velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;
            transform.rotation = new Quaternion();

            if (vertical > 0)
            {
                _direction = Direction.North;
            }
            else if (vertical < 0)
            {
                _direction = Direction.South;
            }
            else if (horizontal > 0)
            {
                _direction = Direction.East;
            }
            else if (horizontal < 0)
            {
                _direction = Direction.West;
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}