using PlayFab.ClientModels;
using PlayFab;
using System;
using UnityEngine;

namespace Lesson4
{
    public class PlayfabLoginLesson4 : MonoBehaviour
    {
        private const string AuthGuidKey = "auth_guid_key";

        private void Start()
        {
            if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
                PlayFabSettings.staticSettings.TitleId = "F22B0";

            var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
            var id = PlayerPrefs.GetString(AuthGuidKey, Guid.NewGuid().ToString());

            var request = new LoginWithCustomIDRequest
            {
                CustomId = "GeekBrainsLesson3",
                CreateAccount = true
            };

            PlayFabClientAPI.LoginWithCustomID(request,
                result =>
                {
                    PlayerPrefs.SetString(AuthGuidKey, id);
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