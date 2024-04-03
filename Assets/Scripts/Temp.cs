using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Hieu
{
    public class Temp : NetworkBehaviour
    {
        public NetworkObject _networkObjectPlayer;
        [SerializeField] ObjectContactDisplay _objectContactDisplay;
        [Space]
        [SerializeField] Transform _objectContact;
        [SerializeField] Transform _objContactHolder;
        [Space]
        [SerializeField] bool _canPlant;
        [SerializeField] bool _isDistance;
        [SerializeField] bool _isCheckGround;
        [Space]
        [SerializeField] String _groundTag = "Ground";
        [SerializeField] Transform _models;
        [SerializeField] Material _green, _red;
        [Space]
        [SerializeField] BoxSensor _sensorCheckCollider;
        [SerializeField] BoxSensor _sensorCheckAround;

        public bool _IsDistance { get => _isDistance; set => _isDistance = value; }
        public ObjectContactDisplay _ObjectContactDisplay { get => _objectContactDisplay; set => _objectContactDisplay = value; }

        private void Awake()
        {
            
        }

        private void Update()
        {
            if (!_networkObjectPlayer.IsOwner) return;
            
            PlantPrefab();
        }

        private void FixedUpdate()
        {
            if (!_networkObjectPlayer.IsOwner) return;

            CheckPlant();
            SetMaterial();
        }

        private void PlantPrefab()
        {
            if (Input.GetMouseButtonDown(0) && _canPlant)
            {
                gameObject.SetActive(false);

                ObjectContact o = Instantiate(_objectContact, this.transform.position, this.transform.rotation, _objContactHolder).GetComponent<ObjectContact>();

                o._Models.transform.rotation = _models.rotation;
                o._ObjectContactDisplay = _objectContactDisplay;

            }
        }

        private void SetMaterial()
        {
            if (_canPlant)
            {
                _models.GetComponent<Renderer>().material = _green;
            }
            else
            {
                _models.GetComponent<Renderer>().material = _red;
            }
        }

        private void CheckPlant()
        {
            if (_sensorCheckCollider._Hits.Count == 0 && IsTouchGround() && _isDistance)
            {
                _canPlant = true;
            }
            else
            {
                _canPlant = false;

            }
        }

        bool IsTouchGround()
        {
            if (_isCheckGround == false) return true;

            // Làm thế nào để cái sensor check ở dưới
            foreach (var obj in _sensorCheckAround._Hits)
            {
                if (obj.CompareTag(_groundTag))
                {
                    return true;
                }
            }

            return false;
        }
    }
}