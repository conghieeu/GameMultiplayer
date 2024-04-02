using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hieu
{
    public class BoxSensor : MonoBehaviour
    {
        [SerializeField] List<Transform> _hits;
        [SerializeField] List<String> _tags;
        [SerializeField] Vector3 _size;

        public List<Transform> _Hits { get => _hits; }

        private void FixedUpdate()
        {
            DetectTarget();
        }

        void DetectTarget()
        {
            RaycastHit[] hits = Physics.BoxCastAll(transform.position, _size / 2f, transform.forward, transform.rotation, 0f);

            List<Transform> hit = hits.Select(x => x.transform).ToList();

            _hits = hit;
        }

        protected void OnDrawGizmosSelected()
        {
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, _size);
            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(Vector3.zero, _size);
        }
    }
}