using Unity.Netcode;
using UnityEngine;

namespace Hieu.Network
{
    public class BootstrapPlayer : NetworkBehaviour
    {
        public Vector3 _spawnPos;

        private void Update()
        {
            if (!IsOwner) return;

            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log($"Di chuyển player tới chỗ chỉ định 1");

                MovePlayer();

            }
        }

        private void MovePlayer()
        {
            Debug.Log($"Di chuyển player tới chỗ chỉ định 2");
            transform.position = _spawnPos;
        }
    }
}
