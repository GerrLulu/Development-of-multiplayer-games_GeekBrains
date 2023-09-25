using PlayFab.ClientModels;
using PlayFab;
using System;
using UnityEngine;

namespace Lesson4
{
    public class PlayfabLoginLesson4 : MonoBehaviour
    {
        private const string AUTH_GUID_KEY = "auth_guid_key";

        private void Start()
        {
            if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
                PlayFabSettings.staticSettings.TitleId = "F22B0";

            var needCreation = PlayerPrefs.HasKey(AUTH_GUID_KEY);
            var id = PlayerPrefs.GetString(AUTH_GUID_KEY, Guid.NewGuid().ToString());

            var request = new LoginWithCustomIDRequest
            {
                CustomId = "GeekBrainsLesson3",
                CreateAccount = true
            };

            PlayFabClientAPI.LoginWithCustomID(request,
                result =>
                {
                    PlayerPrefs.SetString(AUTH_GUID_KEY, id);
                    OnLoginSuccess(result);
                }, OnLoginError);
        }


        private void OnLoginSuccess(LoginResult result)
        {
            Debug.Log("Congratulations, you made successful API call!");
        }

        private void OnLoginError(PlayFabError error)
        {
            var errorMessage = error.GenerateErrorReport();
            Debug.LogError($"Something went wrong: {errorMessage}");
        }
    }
}