using Gummi;
using UnityEngine;

namespace Game.Play
{
    public class Grabber : MonoBehaviour
    {
        [SerializeField]
        Vector2 _handRange = new(1.5f, 3f);

        [SerializeField]
        [Min(0)]
        float _scrollSpeed = 25f;

        [SerializeField]
        [Min(0)]
        float _turnSpeed = 5f;
        
        [SerializeField]
        float _throwForce = 500f;
        
        [SerializeField]
        Camera _camera;

        [HideInInspector]
        public Player_Controller _player;
        
        [SerializeField]
        Transform _hand;

        [SerializeField, ReadOnly]
        Grabbable _item;
        
        #region MonoBehaviour
        #if UNITY_EDITOR
        void OnValidate()
        {
            _camera = GetComponentInChildren<Camera>();
            _player = GetComponent<Player_Controller>();
        }
        #endif
        
        void Update()
        {
            // try to move the hand
            var input = GameManager.Instance.input.gameplay;
            TryScroll(input.Scroll);
            
            // try to throw the item
            InteractionType @throw = input.Throw;
            if (@throw is InteractionType.Down)
            {
                Throw();
                return;
            }
            
            // try to rotate the item
            float rotation = input.Rotate;
            TryRotate(-rotation);
            
            // try to grab an item
            InteractionType grab = input.Grab;
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
            Ray ray = new(camTransform.position, camTransform.forward);
            bool canGrab = Physics.Raycast(ray, out RaycastHit hitinfo, _handRange.y + 1);
            
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

            float scrollPos = Mathf.Clamp(Vector3.Distance(item.transform.position, _camera.transform.position), _handRange.x, _handRange.y);
            _hand.localPosition += Vector3.forward * (-_hand.localPosition.z + scrollPos);

            item.Grab(this, _hand);
        }

        public void Release()
        {
            if (!_item) return;

            _item.Release();
            _item = null;
        }

        public void Throw()
        {
            if (!_item) return;

            _item.Throw(_throwForce);
            _item = null;
        }

        void TryScroll(float amount)
        {
            // exit, there is no item to move
            if (!_item) return;

            if (amount == 0) return;
            
            Scroll(amount);
        }

        void Scroll(float amount)
        {
            amount *= Time.deltaTime;

            Vector3 nPos = _hand.localPosition;
            nPos.z = Mathf.MoveTowards(nPos.z, nPos.z + amount, _scrollSpeed * Time.deltaTime);
            nPos.z = Mathf.Clamp(nPos.z, _handRange.x, _handRange.y);
            
            _hand.localPosition = nPos;
        }

        void TryRotate(float amount)
        {
            if (!_item) return;
            
            if (amount == 0) return;
            
            Rotate(amount);
        }

        void Rotate(float amount)
        {
            float mult = amount * _turnSpeed * Time.deltaTime;
            _item.Rotate(_camera.transform.forward * mult);
        }
        
        #if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (_camera == null) return;

            var camTransform = _camera.transform;
            var forward = camTransform.forward;
            
            Vector3 start = camTransform.position;
            Vector3 end = start + forward * (_handRange.y + 1);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(start, end);

            end = start + forward * _handRange.y;
            start += forward * _handRange.x;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(start, end);
            
            Vector3 center = _hand.position;
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(center, 0.25f);
        }
        #endif
    }
}