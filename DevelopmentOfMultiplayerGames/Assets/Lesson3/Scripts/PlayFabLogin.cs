using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lesson3
{
    public class PlayFabLogin : MonoBehaviour
    {
        [SerializeField] private Button _logInButton;
        [SerializeField] private TextMeshProUGUI _textConnected;

        LoginWithCustomIDRequest _request;


        private void Start()
        {
            if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            {
                PlayFabSettings.staticSettings.TitleId = "F22B0";
            }

            _logInButton.onClick.AddListener(() => Connect());
        }


        private void Connect()
        {
            _request = new LoginWithCustomIDRequest
            {
                CustomId = "GeekBrainsLesson3",
                CreateAccount = true
            };

            PlayFabClientAPI.LoginWithCustomID(_request, OnLoginSuccess, OnLoginFailure);
        }

        private void OnLoginSuccess(LoginResult result)
        {
            Debug.Log("Congratulations, you made successful API call!");

            _textConnected.text = "Conected";
            _textConnected.color = Color.green;

            SetUserData(result.PlayFabId);

            //MakePurchase();
            GetInventory();
        }

        private void GetInventory()
        {
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), result => ShowInventory(result.Inventory),
                OnLoginFailure);
        }

        private void ShowInventory(List<ItemInstance> inventory)
        {
            var firstItem = inventory.First();
            Debug.Log($"{firstItem.ItemId}");
            ConsumePotion(firstItem.ItemInstanceId);
        }

        private void ConsumePotion(string itemInstanceId)
        {
            PlayFabClientAPI.ConsumeItem(new ConsumeItemRequest
            {
                ConsumeCount = 1,
                ItemInstanceId = itemInstanceId
            },
            result =>
            {
                Debug.Log("Complete ConsumeItem");
            },
            OnLoginFailure);
        }

        private void MakePurchase()
        {
            PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest
            {
                CatalogVersion = "main",
                ItemId = "health_potion",
                Price = 3,
                VirtualCurrency = "SC"
            },
            result =>
            {
                Debug.Log("Complete PurchaseItem");
            },
            OnLoginFailure);
        }

        private void SetUserData(string playFabId)
        {
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { "time_recieve_daile_reward", DateTime.UtcNow.ToString() }
                }
            },
            result =>
            {
                Debug.Log("SetUserData");
                GetUserData(playFabId, "time_recieve_daile_reward");
            },
            OnLoginFailure);
        }

        private void GetUserData(string playFabId, string keyData)
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest
            {
                PlayFabId = playFabId
            }, result =>
            {
                if (result.Data.ContainsKey(keyData))
                    Debug.Log($"{keyData}: {result.Data[keyData].Value}");
            }, OnLoginFailure);
        }

        private void OnLoginFailure(PlayFabError error)
        {
            var errorMessage = error.GenerateErrorReport();
            Debug.LogError($"Something went wrong: {errorMessage}");

            _textConnected.text = "Not conected";
            _textConnected.color = Color.red;
        }

        private void OnDisable()
        {
            _logInButton.onClick.RemoveAllListeners();
        }
    }
}