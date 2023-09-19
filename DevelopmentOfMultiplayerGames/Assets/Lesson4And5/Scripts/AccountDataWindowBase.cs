using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Lesson4
{
    public class AccountDataWindowBase : MonoBehaviour
    {
        [SerializeField] private InputField _userNameField;
        [SerializeField] private InputField _passwordField;

        [SerializeField] private Button _backButtton;

        [SerializeField] private Canvas _enterInGameCanvas;
        [SerializeField] private Canvas _createAccountCanvas;
        [SerializeField] private Canvas _singInCanvas;

        protected string _userName;
        protected string _password;

        protected bool _isLoading;


        private void Start()
        {
            SubscriptionsElementsUi();
        }


        protected virtual void SubscriptionsElementsUi()
        {
            _userNameField.onValueChanged.AddListener(UpdateUsername);
            _passwordField.onValueChanged.AddListener(UpdatePassword);

            _backButtton.onClick.AddListener(BackInGameWindow);
        }


        private void UpdateUsername(string userName)
        {
            _userName = userName;
        }

        private void UpdatePassword(string password)
        {
            _password = password;
        }

        protected void EnterInGameScene()
        {
            SceneManager.LoadScene(1);
        }

        private void BackInGameWindow()
        {
            _singInCanvas.enabled = false;
            _createAccountCanvas.enabled = false;
            _enterInGameCanvas.enabled = true;
        }
    }
}