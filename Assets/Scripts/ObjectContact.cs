using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hieu
{
    public class ObjectContact : MonoBehaviour
    {
        [SerializeField] ObjectContactDisplay _objectContactDisplay;
        [SerializeField] Transform _models;
        [SerializeField] Transform _tempPrefab;

        public Transform _Models { get => _models; set => _models = value; }
        public ObjectContactDisplay _ObjectContactDisplay { get => _objectContactDisplay; set => _objectContactDisplay = value; }

        public void InstantTemp()
        {
            Destroy(this.gameObject);

            // Set Temp 
            Temp temp = Temp.Instance;
            temp.gameObject.SetActive(true);
            temp._ObjectContactDisplay = _objectContactDisplay;
            
            

        }
    }

}