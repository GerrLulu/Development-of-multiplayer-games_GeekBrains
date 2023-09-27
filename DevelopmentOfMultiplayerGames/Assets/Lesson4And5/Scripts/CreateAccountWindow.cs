using Lesson5;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lesson4
{
    public class CreateAccountWindow : AccountDataWindowBase
    {
        [SerializeField] private InputField _mailField;
        [SerializeField] private Button _createAccountButton;
        [SerializeField] private TMP_Text _titleLabel;

        private string _mail;


        protected override void SubscriptionsElementsUi()
        {
            base.SubscriptionsElementsUi();

            _mailField.onValueChanged.AddListener(UpdateMail);
            _createAccountButton.onClick.AddListener(CreateAccount);
        }

        private void CreateAccount()
        {
            var loadUi = new LoadingUi(_titleLabel);
            loadUi.StartLoad();

            PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
            {
                Username = _userName,
                Email = _mail,
                Password = _password
            }, result =>
            {
                loadUi.StopLoad();
                Debug.Log($"Success: {_userName}");
                EnterInGameScene();
            }, error =>
            {
                Debug.LogError($"Fail: {error.ErrorMessage}");
            });
        }

        private void UpdateMail(string mail)
        {
            _mail = mail;
        }
    }
}