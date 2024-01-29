using System;
using System.Collections;
using UnityEngine;

namespace Village
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public abstract class DroppableItem : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private float _captureDelay;

        private Rigidbody _body;
        private Collider _collider;
        private bool _capture;
        private bool _captured;

        public bool CanCapture => !_captured && _capture;

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();

            StartCoroutine(SetCaptureAfterDelay());
        }

        private IEnumerator SetCaptureAfterDelay()
        {
            yield return new WaitForSeconds(_captureDelay);
            _capture = true;
        }

        public void Push(Vector3 direction)
        {
            _body.AddForce(direction, ForceMode.Impulse);

            StartCoroutine(DisableBodyWhenStop());
        }

        public virtual Item Capture()
        {
            if (!CanCapture)
                throw new InvalidOperationException();

            _captured = true;
            DisableGravity();

            return _item;
        }

        private IEnumerator DisableBodyWhenStop()
        {
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => _body.velocity == Vector3.zero);

            DisableBody();
        }

        private void DisableBody()
        {
            _body.isKinematic = true;
        }

        private void DisableGravity()
        {
            DisableBody();
            _body.useGravity = false;
            _collider.enabled = false;
        }
    }
}