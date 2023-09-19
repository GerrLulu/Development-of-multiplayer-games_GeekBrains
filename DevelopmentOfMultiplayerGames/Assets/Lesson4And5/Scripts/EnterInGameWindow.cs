using UnityEngine;
using UnityEngine.UI;

namespace Lesson4
{
    public class EnterInGameWindow : MonoBehaviour
    {
        [SerializeField] Button _singInButton;
        [SerializeField] Button _createAccountButton;

        [SerializeField] Canvas _enterInGameCanvas;
        [SerializeField] Canvas _createAccountCanvas;
        [SerializeField] Canvas _singInCanvas;


        private void Start()
        {
            _singInButton.onClick.AddListener(OpenSingInWindow);
            _createAccountButton.onClick.AddListener(OpenCreateAccountWindow);
        }

        private void OpenSingInWindow()
        {
            _singInCanvas.enabled = true;
            _enterInGameCanvas.enabled = false;
        }

        private void OpenCreateAccountWindow()
        {
            _createAccountCanvas.enabled = true;
            _enterInGameCanvas.enabled = false;
        }
    }
}