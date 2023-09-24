using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lesson7
{
    public class LaunchManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _controlPanel;
        [SerializeField] private Text _feedbackText;
        [SerializeField] private byte _maxPlayersRoom = 4;
        [SerializeField] private Button _buttonPlay;
        [SerializeField] private InputField _inputField;

        private const string PLAYER_NAME_PREF_KEY = "PlayerName";

        private bool _isConnecting;
        private string _gameVersion = "1";


        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            _buttonPlay.onClick.AddListener(Connect);


        }

        private void OnDestroy()
        {
            _buttonPlay.onClick.RemoveAllListeners();
        }


        public void Connect()
        {
            _feedbackText.text = "";
            _isConnecting = true;
            _controlPanel.SetActive(false);

            if(PhotonNetwork.IsConnected)
            {
                LogFeedback("Joining Room...");
                SetPlayerName(_inputField.text);
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                LogFeedback("Connecting...");
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = _gameVersion;
            }
        }

        public void SetPlayerName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                PhotonNetwork.NickName = PLAYER_NAME_PREF_KEY;
            }
            PhotonNetwork.NickName = value;
        }

        void LogFeedback(string message)
        {
            if (_feedbackText == null)
            {
                return;
            }

            _feedbackText.text += Environment.NewLine + message;
        }


        public override void OnConnectedToMaster()
        {
            if(_isConnecting)
            {
                LogFeedback("OnConnectedToMaster: Next -> try to Join Random Room");
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            LogFeedback("<Color=Red>OnJoinRandomFailed</Color>: Next -> Create a new Room");
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = _maxPlayersRoom });
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            LogFeedback("<Color=Red>OnDisconnected</Color> " + cause);
            _isConnecting = false;
            _controlPanel.SetActive(true);
        }

        public override void OnJoinedRoom()
        {
            LogFeedback("<Color=Green>OnJoinedRoom</Color> with " + PhotonNetwork.CurrentRoom.PlayerCount + " Player(s)");
        }
    }
}