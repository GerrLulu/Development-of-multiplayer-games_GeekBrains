using Lesson8;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace Lesson4
{
    public class PlayFabAccountManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleLabel;
        [SerializeField] private Transform _transform;
        [SerializeField] private GameObject _newCharacterCreatePanel;
        [SerializeField] private Button _createCharacterButton;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private List<SlotCharacterWidget> _slots;

        private TMP_Text _textInfoCatalog;
        private bool _isLoading = true;
        private string _characterName;


        private void Start ()
        {
            StartCoroutine(Load());
            PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);
            PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), OnGetCatalogSuccess, OnError);

            GetCharacters();

            foreach (SlotCharacterWidget slot in _slots)
                slot.SlotButton.onClick.AddListener(OpenCreateNewCharacter);

            _inputField.onValueChanged.AddListener(OnNameChanged);
            _createCharacterButton.onClick.AddListener(CreateCharacter);
        }

        private void CreateCharacter()
        {
            PlayFabClientAPI.GrantCharacterToUser(new GrantCharacterToUserRequest
            {
                CharacterName = _characterName,
                ItemId = "character_token"
            }, result =>
            {
                UpdateCharacterStatistics(result.CharacterId);
            }, OnError);
        }

        private void UpdateCharacterStatistics(string characterId)
        {
            PlayFabClientAPI.UpdateCharacterStatistics(new UpdateCharacterStatisticsRequest
            {
                CharacterId = characterId,
                CharacterStatistics = new Dictionary<string, int>
                {
                    {"Level", 1},
                    {"Gold  ", 0}
                }
            }, result =>
            {
                Debug.Log($"Complete");
                CloseCreateNewCharacter();
                GetCharacters();
            }, OnError);
        }

        private void OnNameChanged(string changedName)
        {
            _characterName = changedName;
        }

        private void OpenCreateNewCharacter()
        {
            _newCharacterCreatePanel.SetActive(true);
        }

        private void CloseCreateNewCharacter()
        {
            _newCharacterCreatePanel.SetActive(false);
        }

        private void GetCharacters()
        {
            PlayFabClientAPI.GetAllUsersCharacters(new ListUsersCharactersRequest(),
                result =>
                {
                    Debug.Log($"Characters owned: + {result.Characters.Count}");
                    ShowCharactersInSlot(result.Characters);
                }, OnError);
        }

        private void ShowCharactersInSlot(List<CharacterResult> characters)
        {
            if(characters.Count == 0)
            {
                foreach(var slot in _slots)
                    slot.ShowEptySlot();
            }
            else if(characters.Count > 0 && characters.Count < _slots.Count)
            {
                PlayFabClientAPI.GetCharacterStatistics(new GetCharacterStatisticsRequest
                {
                    CharacterId = characters.First().CharacterId
                }, result =>
                {
                    var level = result.CharacterStatistics["Level"].ToString();
                    var gold = result.CharacterStatistics["Gold"].ToString();

                    _slots.First().ShowInfoCharacter(characters.First().CharacterName, level, gold);
                }, OnError); ;
            }
            else
            {
                Debug.Log("Add slots");
            }
        }

        private void OnGetAccount(GetAccountInfoResult result)
        {
            _isLoading = false;
            _titleLabel.text = $"Playfab id: {result.AccountInfo.PlayFabId}\n" +
                $"Playfab name: {result.AccountInfo.Username}";
        }

        private void OnError(PlayFabError error)
        {
            var errorMessage = error.GenerateErrorReport();
            Debug.Log(errorMessage);
        }

        private IEnumerator Load()
        {
            while( _isLoading)
            {
                _titleLabel.text = "Loading .";
                yield return new WaitForSeconds(0.1f);
                _titleLabel.text = "Loading ..";
                yield return new WaitForSeconds(0.1f);
                _titleLabel.text = "Loading ...";
                yield return new WaitForSeconds(0.1f);
            }
        }


        private void OnGetCatalogSuccess(GetCatalogItemsResult result)
        {
            Debug.Log("OnGetCatalogSuccess");
            ShowItems(result.Catalog);
        }

        private void ShowItems(List<CatalogItem> catalog)
        {
            foreach (CatalogItem item in catalog)
            {
                Instantiate(_titleLabel, _transform);
                _textInfoCatalog.text = item.DisplayName;

                Debug.Log($"{item.ItemId}");
            }
        }
    }
}