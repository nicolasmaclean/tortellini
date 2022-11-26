using System;
using Gummi;
using UnityEngine;

namespace Game.Play
{
    [RequireComponent(typeof(Rigidbody))]
    public class Grabbable : MonoBehaviour
    {
        public bool Grabbed => _hand;

        [SerializeField]
        [Min(0)]
        float _minBreakingForce = 1;

        [SerializeField]
        [Range(0, 1)]
        float _lerpSpeed = 0.75f;
        float _lerpTime;

        Rigidbody _rb;
        
        Transform _hand;
        Grabber _grabber;
        Player_Controller _player;

        Vector3 _startPos;
        Quaternion _startRot;
        
        #region MonoBehaviour
        void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            _startPos = transform.position;
            _startRot = transform.rotation;
        }
        
        void Update()
        {
            // Failsafe Respawn
            if (_rb.position.y <= -100)
            {
                _rb.position = _startPos;
                _rb.rotation = _startRot;
                _rb.velocity = Vector3.zero;
            }

            // exit, this item is not being held
            if (!Grabbed) return;

            MoveTowardsHand();

            _rb.velocity = Vector3.zero;
        }

        void OnCollisionEnter(Collision collision)
        {
            // exit, this item is not being held
            if (!Grabbed) return;

            // exit, force was not big enough
            if (collision.impulse.sqrMagnitude < _minBreakingForce * _minBreakingForce) return;

            _grabber.Release();
        }
        #endregion

        public void Grab(Grabber grabber, Transform hand)
        {
            _rb.useGravity = false;
            _rb.velocity = Vector3.zero;

            _hand = hand;
            _grabber = grabber;
            if (_grabber != null) _player = _grabber._player;
        }

        public void Rotate(Vector3 force)
        {
            _rb.AddTorque(force);
        }

        public void Release()
        {
            _hand = null;
            _player = null;
            _lerpTime = 0;
            _rb.useGravity = true;
        }

        // Release, but also apply force along _hand.transform.forward
        public void Throw(float force=100)
        {
            Vector3 dir = _hand.transform.forward;
            Release();
            
            // apply force
            _rb.AddForce(dir * force);
        }

        void MoveTowardsHand()
        {
            _lerpTime = _lerpTime + Time.deltaTime * 2 < 1 ? _lerpTime + Time.deltaTime * 2 : 1;
            Vector3 nPos = Vector3.Lerp(transform.position, _hand.position, _lerpTime * _lerpSpeed * Time.deltaTime * 60);
            _rb.MovePosition(nPos);
            
            // Rotate item with player Y rotation
            if (_player != null) _rb.MoveRotation(Quaternion.Euler(Vector3.up * _player.lookYDelta) * _rb.rotation);
        }
    }
}