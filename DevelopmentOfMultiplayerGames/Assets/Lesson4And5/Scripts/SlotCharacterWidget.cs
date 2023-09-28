using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lesson8
{
    public class SlotCharacterWidget : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _emptySlot;
        [SerializeField] private GameObject _infoCharacterSlot;
        [SerializeField] private TMP_Text _nameLabel;
        [SerializeField] private TMP_Text _levelLabel;
        [SerializeField] private TMP_Text _damageLabel;
        [SerializeField] private TMP_Text _hpLabel;
        [SerializeField] private TMP_Text _xpLabel;
        [SerializeField] private TMP_Text _goldLabel;

        public Button SlotButton => _button;


        public void ShowInfoCharacter(string name, string level, string damage, string hp, string xp, string gold)
        {
            _nameLabel.text = "Name:" + name;
            _levelLabel.text = "Level:" + level;
            _damageLabel.text = "Damage:" + damage;
            _hpLabel.text = "HP:" + hp;
            _xpLabel.text = "XP:" + xp;
            _goldLabel.text = "Gold:" + gold;

            _infoCharacterSlot.SetActive(true);
            _emptySlot.SetActive(false);
        }

        public void ShowEptySlot()
        {
            _infoCharacterSlot.SetActive(false);
            _emptySlot.SetActive(true);
        }
    }
}