using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Lesson4
{
    public class SingInWindow : AccountDataWindowBase
    {
        [SerializeField] private Button _singInButton;

        [SerializeField] private TMP_Text _titleLabel;


        protected override void SubscriptionsElementsUi()
        {
            base.SubscriptionsElementsUi();

            _singInButton.onClick.AddListener(SingIn);
        }

        private void SingIn()
        {
            _isLoading = true;
            StartCoroutine(Load());

            PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
            {
                Username = _userName,
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