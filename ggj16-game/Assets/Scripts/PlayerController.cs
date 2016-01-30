using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private Direction _direction;
        private Animator _animator;
        private Rigidbody _rigidbody;
        private GameObject _cameraAnchor;

        private bool _controlsEnabled = true;

        [SerializeField] private float _maxSpeed;

        void Start ()
        {
            _animator = transform.FindChild("Renderer").GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            _cameraAnchor = GameObject.Find("CameraAnchor");
        }
	
        void FixedUpdate ()
        {
            if (_controlsEnabled && UpdateInput())
            {
                _animator.SetInteger("Direction", (int) _direction);
            }
            _cameraAnchor.transform.position = transform.position;
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

            if (_rigidbody.velocity.magnitude < 0.01f)
            {
                _animator.speed = 0f;
            }
            else
            {
                if (_animator.speed < 1f)
                {
                    _animator.speed = 1f;
                }
            }

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

        public void DisableControls()
        {
            _controlsEnabled = false;
        }
    }
}