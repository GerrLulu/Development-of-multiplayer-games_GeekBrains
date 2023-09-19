using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace Lesson6
{
    public class HomeWorkLesson6 : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
    {
        [SerializeField] private ServerSettings _serverSettings;

        private LoadBalancingClient _loadBalancingClient;
        private TypedLobby defailtLobby = new TypedLobby("default lobby", LobbyType.Default);

        private Dictionary<string, RoomInfo> _cachedRoomList = new Dictionary<string, RoomInfo>();


        private void Start ()
        {
            _loadBalancingClient = new LoadBalancingClient();
            _loadBalancingClient.AddCallbackTarget(this);
            _loadBalancingClient.ConnectUsingSettings(_serverSettings.AppSettings);
        }

        private void Update ()
        {
            if (_loadBalancingClient == null)
                return;

            _loadBalancingClient.Service();
        }


        private void UpdateCachedRoomList(List<RoomInfo> roomList)
        {
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
                }
            }
        }


        public void OnConnected()
        {
        }

        public void OnConnectedToMaster()
        {
        }

        public void OnCreatedRoom()
        {
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
            _cachedRoomList.Clear();
        }

        public void OnJoinedRoom()
        {
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
            UpdateCachedRoomList(roomList);
        }
    }
}