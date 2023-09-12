using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Lesson4
{
    public class PlayFabAccountManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleLabel;

        private bool _isLoading = true;


        private void Start ()
        {
            StartCoroutine(Load());
            PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);
        }

        private void OnGetAccount(GetAccountInfoResult result)
        {
            _isLoading = false;
            _titleLabel.text = $"Playfab id: {result.AccountInfo.PlayFabId}\n" +
                $"Playfab name: {result.AccountInfo.Username}\n" +
                $"ServerCustomIdInfo info: {result.AccountInfo.ServerCustomIdInfo}";
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
    }
}