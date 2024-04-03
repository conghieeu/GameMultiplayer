
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Hieu;

namespace Hieu.Network
{
    public class BootstrapPlayer : NetworkBehaviour
    {
        public Vector3 _spawnPos;

        FirstPersonController _firstPersonController;

        private void Awake()
        {
            _firstPersonController = GetComponentInChildren<FirstPersonController>();
        }

        private void Update()
        {
            if (!IsOwner)
            {
                _firstPersonController.playerCamera.enabled = false;
                return;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {

                MovePlayer();

            }
        }

        private void MovePlayer()
        {
            transform.position = _spawnPos;
        }
    }
}
