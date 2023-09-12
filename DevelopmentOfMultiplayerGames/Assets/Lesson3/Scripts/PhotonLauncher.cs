using Photon.Pun;
using System.Collections;
using UnityEngine;

namespace Lesson3
{
    public class PhotonLauncher : MonoBehaviourPunCallbacks
    {
        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            Connect();
        }


        private void Connect()
        {
            if (PhotonNetwork.IsConnected)
                return;

            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = Application.version;

            StartCoroutine(PhotonDisconect());
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");
            PhotonNetwork.CreateRoom("NewRoom");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom");
        }

        private IEnumerator PhotonDisconect()
        {
            yield return new WaitForSeconds(10);

            if (PhotonNetwork.IsConnected)
                PhotonNetwork.Disconnect();

            Debug.Log("Disconnect");
        }
    }
}