using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

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
            _isLoading = true;
            StartCoroutine(Load());

            PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
            {
                Username = _userName,
                Email = _mail,
                Password = _password
            }, result =>
            {
                _isLoading = false;
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

        private IEnumerator Load()
        {
            while (_isLoading)
            {
                _titleLabel.text = "Loading .";
                yield return new WaitForSeconds(0.1f);
                _titleLabel.text = "Loading ..";
                yield return new WaitForSeconds(0.1f);
                _titleLabel.text = "Loading ...";
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}