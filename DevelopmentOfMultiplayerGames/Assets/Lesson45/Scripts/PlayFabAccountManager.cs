using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Lesson4
{
    public class PlayFabAccountManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleLabel;
        [SerializeField] private Transform _transform;

        private TMP_Text _textInfoCatalog;

        private bool _isLoading = true;


        private void Start ()
        {
            StartCoroutine(Load());
            PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);

            PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), OnGetCatalogSuccess, OnError);
        }


        private void OnGetAccount(GetAccountInfoResult result)
        {
            _isLoading = false;
            _titleLabel.text = $"Playfab id: {result.AccountInfo.PlayFabId}\n" +
                $"Playfab name: {result.AccountInfo.Username}";
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


        private void OnGetCatalogSuccess(GetCatalogItemsResult result)
        {
            Debug.Log("OnGetCatalogSuccess");
            ShowItems(result.Catalog);
        }

        private void ShowItems(List<CatalogItem> catalog)
        {
            foreach (CatalogItem item in catalog)
            {
                Instantiate(_titleLabel, _transform);
                _textInfoCatalog.text = item.DisplayName;

                Debug.Log($"{item.ItemId}");
            }
        }
    }
}