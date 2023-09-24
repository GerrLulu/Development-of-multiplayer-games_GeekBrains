using UnityEngine;

namespace Lesson7
{
    public class CameraWorkLesson7 : MonoBehaviour
    {
        [SerializeField] private float _distance = 7.0f;
        [SerializeField] private float _height = 3.0f;
        [SerializeField] private Vector3 _centerOffset = Vector3.zero;
        [SerializeField] private bool _followOnStart = false;
        [SerializeField] private float _smoothSpeed = 0.125f;

        private Transform _cameraTransform;
        private bool _isFollowing;
        private Vector3 _cameraOffset = Vector3.zero;


        private void Start()
        {
            if (_followOnStart)
            {
                OnStartFollowing();
            }
        }

        private void LateUpdate()
        {
            if (_cameraTransform == null && _isFollowing)
            {
                OnStartFollowing();
            }

            if (_isFollowing)
            {
                Follow();
            }
        }


        public void OnStartFollowing()
        {
            _cameraTransform = Camera.main.transform;
            _isFollowing = true;

            Cut();
        }

        private void Follow()
        {
            _cameraOffset.z = -_distance;
            _cameraOffset.y = _height;

            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, transform.position + transform.
                TransformVector(_cameraOffset), _smoothSpeed * Time.deltaTime);

            _cameraTransform.LookAt(transform.position + _centerOffset);

        }

        private void Cut()
        {
            _cameraOffset.z = -_distance;
            _cameraOffset.y = _height;

            _cameraTransform.position = transform.position + transform.TransformVector(_cameraOffset);

            _cameraTransform.LookAt(transform.position + _centerOffset);
        }
    }
}