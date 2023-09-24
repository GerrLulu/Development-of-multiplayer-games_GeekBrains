using UnityEngine;
using UnityEngine.UI;

namespace Lesson7
{
    public class UiForPlayer : MonoBehaviour
    {
        [SerializeField] private Vector3 _screenOffset = new Vector3(0f, 30f, 0f);
        [SerializeField] private Text _playerNameText;
        [SerializeField] private Slider _playerHealthSlider;
        [SerializeField] private Text _idText;
        [SerializeField] private CanvasGroup _canvasGroup;

        private PlayerManagerLesson7 _target;
        private float _characterControllerHeight;
        private Transform _targetTransform;
        private Renderer _targetRenderer;
        private Vector3 _targetPosition;


        private void Awake()
        {
            transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        }

        void Update()
        {
            if (_target == null)
            {
                Destroy(gameObject);
                return;
            }

            if (_playerHealthSlider != null)
            {
                _playerHealthSlider.value = _target.Health;
            }

            _idText.text = _target.Id.ToString();
        }

        private void LateUpdate()
        {
            if (_targetRenderer != null)
            {
                _canvasGroup.alpha = _targetRenderer.isVisible ? 1f : 0f;
            }

            if (_targetTransform != null)
            {
                _targetPosition = _targetTransform.position;
                _targetPosition.y += _characterControllerHeight;

                transform.position = Camera.main.WorldToScreenPoint(_targetPosition) + _screenOffset;
            }
        }


        public void SetTarget(PlayerManagerLesson7 target)
        {

            if (target == null)
            {
                return;
            }

            _target = target;
            _targetTransform = _target.GetComponent<Transform>();
            _targetRenderer = _target.GetComponentInChildren<Renderer>();

            CharacterController _characterController = _target.GetComponent<CharacterController>();

            if (_characterController != null)
            {
                _characterControllerHeight = _characterController.height;
            }

            if (_playerNameText != null)
            {
                _playerNameText.text = _target.photonView.Owner.NickName;
            }
        }
    }
}