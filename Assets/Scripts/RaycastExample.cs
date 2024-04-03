using Unity.Netcode;
using UnityEngine;

namespace Hieu
{
    public class RaycastExample : NetworkBehaviour
    {
        public NetworkObject _networkObjectPlayer;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private RaycastHit _hit;

        public RaycastHit Hit { get => _hit; }
        public Camera _Camera { get => _camera; }

        void Update()
        {
            if (!_networkObjectPlayer.IsOwner) return;

            if (_camera.enabled)
            {
                GetHitForward();
            }
        }

        private void GetHitForward()
        {
            Vector3 look = _camera.transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(_camera.transform.position, look);

            if (Physics.Raycast(_camera.transform.position, look, out _hit, 20f, _layerMask))
            {
                if (Input.GetKeyDown(KeyCode.E) && _hit.transform.GetComponent<ObjectContact>())
                {
                    _hit.transform.GetComponent<ObjectContact>().InstantTemp();
                }
            }

        }
    }

}