using System.Collections;
using TMPro;
using UnityEngine;

namespace Lesson6
{
    public class ConnectRoomButton : MonoBehaviour
    {
        [SerializeField] TMP_Text _name;


        public ConnectRoomButton(string name)
        {
            _name.text = name;
        }


        private void Start()
        {

        }
    }
}