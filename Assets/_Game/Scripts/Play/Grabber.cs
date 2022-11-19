using System.Collections.Generic;
using Gummi.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Play
{
    public class Grabber : MonoBehaviour
    {
        [SerializeField]
        float _range = 5f;
        
        [SerializeField]
        Camera _camera;
        
        [SerializeField]
        Transform _hand;

        [SerializeField]
        Pool _springPool;

        #if UNITY_EDITOR
        [SerializeField]
        #endif
        List<SpringJoint> _grabbedItems = new();
        
        #region MonoBehaviour
        #if UNITY_EDITOR
        void OnValidate()
        {
            _camera = GetComponentInChildren<Camera>();
        }
        #endif
        
        void Update()
        {
            int interaction = GameManager.Instance.input.gameplay.Interact;
            
            bool released = interaction == 0;
            bool noItemsToRelease = _grabbedItems.Count == 0;
            if (released)
            {
                // exit, no items to release
                if (noItemsToRelease) return;

                Release();
                return;
            }
            
            // exit, grab button wasn't pressed
            bool pressed = interaction == 1;
            if (!pressed) return;
            
            TryGrab();
        }
        #endregion

        void TryGrab()
        {
            // check grabbable item is in front of the user
            var camTransform = _camera.transform;
            Ray ray = new Ray(camTransform.position, camTransform.forward);
            bool canGrab = Physics.Raycast(ray, out RaycastHit hitinfo, 10);
            
            if (!canGrab) return;

            Rigidbody rb = hitinfo.collider.attachedRigidbody; 
            if (!rb) return;
            
            Grabbable item = rb.GetComponent<Grabbable>();
            if (!item) return;
            
            Grab(rb);
        }

        void Grab(Rigidbody item)
        {
            GameObject spring = _springPool.CheckOut();
            SpringJoint joint = spring.GetComponent<SpringJoint>();
            _grabbedItems.Add(joint);
            
            spring.transform.SetParent(_hand);
            spring.transform.localPosition = Vector3.zero;
            spring.transform.localRotation = Quaternion.identity;

            joint.connectedBody = item;
            item.useGravity = false;
        }

        void Release()
        {
            foreach (var spring in _grabbedItems)
            {
                GameObject go = spring.gameObject;
                Rigidbody item = spring.connectedBody;
                item.useGravity = true;
                spring.connectedBody = null;
                
                // ReSharper disable once PossibleInvalidOperationException
                _springPool.CheckIn(go, moveToScene: true);
            }
            
            _grabbedItems.Clear();
        }
        
        #if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (_camera == null) return;

            var camTransform = _camera.transform;
            Vector3 start = camTransform.position;
            Vector3 end = start + camTransform.forward * _range;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(start, end);
        }
        #endif
    }
}