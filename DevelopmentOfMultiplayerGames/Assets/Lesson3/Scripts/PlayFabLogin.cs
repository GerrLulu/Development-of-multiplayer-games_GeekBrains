using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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