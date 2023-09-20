using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lesson6
{
    public class HomeWorkLesson6 : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
    {
        [SerializeField] private ServerSettings _serverSettings;
        [SerializeField] private TMP_Text _stateUiText;

        [SerializeField] private Button _connectButton;
        [SerializeField] private Button _disconnectButton;
        [SerializeField] private Button _updateButton;
        [SerializeField] private Transform _transformPanel;

        private LoadBalancingClient _loadBalancingClient;
        private TypedLobby _defailtLobby = new TypedLobby("default lobby", LobbyType.Default);

        private Dictionary<string, RoomInfo> _cachedRoomList = new Dictionary<string, RoomInfo>();


        private void Start ()
        {
            _loadBalancingClient = new LoadBalancingClient();
            _loadBalancingClient.AddCallbackTarget(this);
            _loadBalancingClient.ConnectUsingSettings(_serverSettings.AppSettings);

            _connectButton.enabled = false;
            _disconnectButton.enabled = false;
            _connectButton.onClick.AddListener(ConnectLobby);
            _disconnectButton.onClick.AddListener(DisconnectLobby);
        }

        private void Update ()
        {
            if (_loadBalancingClient == null)
                return;

            _loadBalancingClient.Service();
            _stateUiText.text = _loadBalancingClient.State.ToString();
        }


        private void ConnectLobby()
        {
            _loadBalancingClient.OpJoinLobby(_defailtLobby);
        }

        private void DisconnectLobby()
        {
            _loadBalancingClient.OpLeaveLobby();
            _connectButton.enabled = true;
            _disconnectButton.enabled = false;
        }

        private void UpdateCachedRoomList(List<RoomInfo> roomList)
        {
            Debug.Log("UpdateCachedRoomList");

            for (int i = 0; i < roomList.Count; i++)
            {
                RoomInfo info = roomList[i];
                if (info.RemovedFromList)
                {
                    _cachedRoomList.Remove(info.Name);
                }
                else
                {
                    _cachedRoomList[info.Name] = info;
                    var connectRoomButton = new ConnectRoomButton(info.Name);
                    Instantiate(connectRoomButton, _transformPanel);
                    Debug.Log(_cachedRoomList[info.Name]);
                }
            }
        }


        public void OnConnected()
        {
        }

        public void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");

            _connectButton.enabled = true;
        }

        public void OnCreatedRoom()
        {
            Debug.Log("OnCreatedRoom");
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            _cachedRoomList.Clear();
        }

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
        }

        public void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");

            _cachedRoomList.Clear();
            _connectButton.enabled = false;
            _disconnectButton.enabled = true;
        }

        public void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom");
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
        }

        public void OnLeftLobby()
        {
            _cachedRoomList.Clear();
        }

        public void OnLeftRoom()
        {
        }

        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log("OnRoomListUpdate");
            UpdateCachedRoomList(roomList);
        }
    }
}