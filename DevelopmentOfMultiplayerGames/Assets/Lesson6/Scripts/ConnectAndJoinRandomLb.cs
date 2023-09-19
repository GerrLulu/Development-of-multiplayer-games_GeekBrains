using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Lesson6
{
    public class ConnectAndJoinRandomLb : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
    {
        [SerializeField] private ServerSettings _serverSettings;
        [SerializeField] private TMP_Text _stateUiText;

        private LoadBalancingClient _loadBalancingClient;
        private TypedLobby _sqlLobby = new TypedLobby("customLobby", LobbyType.SqlLobby);

        public const string GAME_MODE_PROP_KEY = "gm";
        public const string AI_PROP_KEY = "ai";

        public const string MAP_PROP_KEY = "C0";
        public const string GOLD_PROP_KEY = "C1";


        private void Start()
        {
            _loadBalancingClient = new LoadBalancingClient();
            _loadBalancingClient.AddCallbackTarget(this);
            _loadBalancingClient.ConnectUsingSettings(_serverSettings.AppSettings);
        }

        private void Update()
        {
            if (_loadBalancingClient == null)
                return;

            _loadBalancingClient.Service();

            string state = _loadBalancingClient.State.ToString();
            _stateUiText.text = state;
        }

        private void OnDestroy()
        {
            _loadBalancingClient.RemoveCallbackTarget(this);
        }


        public void OnConnected()
        {

        }

        public void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");
            var roomOptions = new RoomOptions
            {
                MaxPlayers = 12,
                PublishUserId = true,
                CustomRoomPropertiesForLobby = new[] { MAP_PROP_KEY, GOLD_PROP_KEY },
                CustomRoomProperties = new Hashtable { { GOLD_PROP_KEY, 400}, { MAP_PROP_KEY, "Map3"} }
            };

            var enterRoomParams = new EnterRoomParams
            {
                RoomName = "NewRoom",
                RoomOptions = roomOptions,
                ExpectedUsers = new[] {"55464"},
                Lobby = _sqlLobby
            };

            _loadBalancingClient.OpCreateRoom(enterRoomParams);
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

        }

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {

        }

        public void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");

            var sqlLobbyFilter = $"{MAP_PROP_KEY} = Map3 and {GOLD_PROP_KEY} between 300 and 400";

            var opJoinRandomRoomParams = new OpJoinRandomRoomParams
            {
                SqlLobbyFilter = sqlLobbyFilter,
            };

            _loadBalancingClient.OpJoinRandomRoom();
        }

        public void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom");
            _loadBalancingClient.CurrentRoom.IsOpen = false;
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed");
            _loadBalancingClient.OpCreateRoom(new EnterRoomParams());
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRoomFailed");
            _loadBalancingClient.OpCreateRoom(new EnterRoomParams());
        }

        public void OnLeftLobby()
        {

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

        }
    }
}