using Photon.Pun;
using UnityEngine;

namespace Lesson7
{
    public class AnimatorManager : MonoBehaviourPun
    {
        [SerializeField] private float directionDampTime = 0.25f;
        [SerializeField] private Animator _animator;


        void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }

            if (!_animator)
            {
                return;
            }

            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Base Layer.Run"))
            {
                if (Input.GetButtonDown("Fire2"))
                    _animator.SetTrigger("Jump");
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (v < 0)
            {
                v = 0;
            }

            _animator.SetFloat("Speed", h * h + v * v);
            _animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
        }
    }
}