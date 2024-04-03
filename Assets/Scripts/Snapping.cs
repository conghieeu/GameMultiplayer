using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Hieu
{
    public class Snapping : NetworkBehaviour
    {
        public NetworkObject _networkObjectPlayer;
        [SerializeField] Transform _temp;
        [SerializeField] bool _enableSnapping; // bật chế độ snapping
        [SerializeField] float _snapDistance = 6f; // Khoảng cách cho phép đặt 
        [SerializeField] float tilesize = 1;
        [SerializeField] Vector3 tileOffset = Vector3.zero;

        public Transform _Temp { get => _temp; }

        RaycastExample _raycastExample;

        private void Awake()
        {
            _raycastExample = GetComponent<RaycastExample>();
            _networkObjectPlayer = GetComponentInParent<NetworkObject>();
        }

        void FixedUpdate()
        {
            if (!_networkObjectPlayer.IsOwner) return;

            if (_Temp)
            {
                SetPointPos();
                SetSnapping();

                if (Vector3.Distance(_raycastExample._Camera.transform.position, _raycastExample.Hit.point) < _snapDistance)
                {
                    _temp.GetComponent<Temp>()._IsDistance = true;
                }
                else
                {
                    _temp.GetComponent<Temp>()._IsDistance = false;
                }
            }
        }

        private void SetSnapping()
        {
            if (_enableSnapping)
            {
                Vector3 currentPosition = _temp.transform.position;

                float snappedX = Mathf.Round(currentPosition.x / tilesize) * tilesize + tileOffset.x;
                float snappedZ = Mathf.Round(currentPosition.z / tilesize) * tilesize + tileOffset.z;
                float snappedY = Mathf.Round(currentPosition.y / tilesize) * tilesize + tileOffset.y;

                Vector3 snappedPosition = new Vector3(snappedX, snappedY, snappedZ);
                _temp.transform.position = snappedPosition;
            }
        }

        private void SetPointPos()
        {
            _temp.position = _raycastExample.Hit.point;
            _temp.rotation = Quaternion.FromToRotation(Vector3.up, _raycastExample.Hit.normal);
        }
    }

}
