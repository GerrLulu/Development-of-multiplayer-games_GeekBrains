using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lesson5;

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
            var loadUi = new LoadingUi(_titleLabel);
            loadUi.StartLoad();

            PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
            {
                Username = _userName,
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
    }
}