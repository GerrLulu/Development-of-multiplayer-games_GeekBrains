using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lesson7
{
    public class PlayerManagerLesson7 : MonoBehaviourPunCallbacks, IPunObservable
    {
        public static GameObject LocalPlayerInstance;
        
        public float Health = 1;

        [SerializeField] private GameObject _playerUiPrefab;
        [SerializeField] private GameObject _beams;
        [SerializeField] private CameraWorkLesson7 _cameraWork;

        private float _id;
        private bool _isFiring;
        private bool _leavingRoom;


        public float Id
        {
            get => photonView.ViewID;
            set => _id = value;
        }


        private void Awake()
        {
            _beams.SetActive(false);

            if (photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (_cameraWork != null)
            {
                if (photonView.IsMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }

            if (_playerUiPrefab != null)
            {
                GameObject _uiGo = Instantiate(_playerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                ProcessInputs();

                if (Health <= 0f && !_leavingRoom)
                {
                    _leavingRoom = PhotonNetwork.LeaveRoom();
                }
            }

            if (_beams != null && _isFiring != _beams.activeInHierarchy)
            {
                _beams.SetActive(_isFiring);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (!other.name.Contains("Beam"))
            {
                return;
            }

            Health -= 0.1f;
        }

        public void OnTriggerStay(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (!other.name.Contains("Beam"))
            {
                return;
            }

            Health -= 0.1f * Time.deltaTime;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode loadingMode)
        {
            CalledOnLevelWasLoaded(scene.buildIndex);
        }

        void CalledOnLevelWasLoaded(int level)
        {
            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }

            GameObject _uiGo = Instantiate(_playerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }

        private void ProcessInputs()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!_isFiring)
                {
                    _isFiring = true;
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                if (_isFiring)
                {
                    _isFiring = false;
                }
            }
        }


        public override void OnLeftRoom()
        {
            _leavingRoom = false;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_isFiring);
                stream.SendNext(Health);
                stream.SendNext(Id);
            }
            else
            {
                _isFiring = (bool)stream.ReceiveNext();
                Health = (float)stream.ReceiveNext();
                Id = (float)stream.ReceiveNext();
            }
        }
    }
}