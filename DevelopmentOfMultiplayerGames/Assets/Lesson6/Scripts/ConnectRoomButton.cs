using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lesson6
{
    public class ConnectRoomButton : MonoBehaviour
    {
        [SerializeField] private Button _conectRoomButton;
        [SerializeField] private TMP_Text _name;

        private RoomInfo _roomInfo;
        private LoadBalancingClient _lbc;
        private TypedLobby _lobby;
        //private int _maxPlayerCount = 2;


        public ConnectRoomButton(RoomInfo info, LoadBalancingClient lbc, TypedLobby typedLobby)
        {
            _roomInfo = info;
            _name.text = info.Name;
            _lbc = lbc;
            _lobby = typedLobby;
        }


        public void Start()
        {
            _conectRoomButton.onClick.AddListener(ConnectRoom);
        }

        private void OnDestroy()
        {
            _conectRoomButton.onClick.RemoveAllListeners();
        }


        private void ConnectRoom()
        {
            var roomOption = new RoomOptions
            {
                MaxPlayers = _roomInfo.MaxPlayers,
                PublishUserId = true,
                IsOpen = _roomInfo.IsOpen
            };


            var enterRoomParams = new EnterRoomParams
            {
                RoomName = _name.text,

                RoomOptions = roomOption,
                Lobby = _lobby
            };

            if (roomOption.IsOpen )
                _lbc.OpJoinRoom(enterRoomParams);
        }
    }
}