using TMPro;
using UnityEngine;

namespace Lesson5
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private TMP_Text _itemText;

        public TMP_Text ItemText
        {
            get { return _itemText; }
            set { _itemText = value; }
        }
    }
}