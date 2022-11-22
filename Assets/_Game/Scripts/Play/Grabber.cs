using Gummi;
using UnityEngine;

namespace Game.Play
{
    public class Grabber : MonoBehaviour
    {
        [SerializeField]
        float _range = 5f;

        [SerializeField]
        float _throwForce = 500f;
        
        [SerializeField]
        Camera _camera;
        
        [SerializeField]
        Transform _hand;

        [SerializeField, ReadOnly]
        Grabbable _item;
        
        #region MonoBehaviour
        #if UNITY_EDITOR
        void OnValidate()
        {
            _camera = GetComponentInChildren<Camera>();
        }
        #endif
        
        void Update()
        {
            // try to throw the item
            InteractionType @throw = GameManager.Instance.input.gameplay.Throw;
            if (@throw is InteractionType.Down)
            {
                Throw();
                return;
            }
            
            // try to grab an item
            InteractionType grab = GameManager.Instance.input.gameplay.Grab;
            if (grab is InteractionType.Up)
            {
                Release();
                return;
            }
            
            // try to grab an item
            if (grab is InteractionType.Down)
            {
                TryGrab();
            }
        }
        #endregion

        void TryGrab()
        {
            // exit, still holding an item
            if (_item) return;
            
            // check grabbable item is in front of the user
            var camTransform = _camera.transform;
            Ray ray = new Ray(camTransform.position, camTransform.forward);
            bool canGrab = Physics.Raycast(ray, out RaycastHit hitinfo, 10);
            
            if (!canGrab) return;

            Rigidbody rb = hitinfo.collider.attachedRigidbody; 
            if (!rb) return;
            
            Grabbable item = rb.GetComponent<Grabbable>();
            if (!item) return;
            
            Grab(item);
        }

        void Grab(Grabbable item)
        {
            _item = item;
            item.Grab(_hand);
        }

        void Release()
        {
            if (!_item) return;
            
            _item.Release();
            _item = null;
        }

        void Throw()
        {
            if (!_item) return;
            
            _item.Throw(_throwForce);
            _item = null;
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