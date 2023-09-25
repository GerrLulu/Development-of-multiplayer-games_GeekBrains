using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Lesson7
{
    public class GameLaunchManager : MonoBehaviourPunCallbacks
    {
        static public GameLaunchManager Instance;

        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Button _quitButton;


        void Start()
        {
            Instance = this;

            _quitButton.onClick.AddListener(LeaveRoom);

            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("Launcher Lesson7");
                return;
            }

            if (_playerPrefab == null)
            {
                return;
            }
            else
            {
                if (PhotonNetwork.InRoom && PlayerManagerLesson7.LocalPlayerInstance == null)
                {
                    PhotonNetwork.Instantiate(_playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                }
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitApplication();
            }
        }

        private void OnDestroy()
        {
            _quitButton.onClick.RemoveAllListeners();
        }


        private void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        private void QuitApplication()
        {
            Application.Quit();
        }

        private void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            PhotonNetwork.LoadLevel("Room for 4 Lesson7");
        }


        public override void OnJoinedRoom()
        {
            if (PlayerManagerLesson7.LocalPlayerInstance == null)
            {
                PhotonNetwork.Instantiate(_playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            }
        }

        public override void OnPlayerEnteredRoom(Player other)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Player other)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                LoadArena();
            }
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("Launcher Lesson7");
        }
    }
}