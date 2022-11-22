﻿using System;
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
        float _minBreakingForce = 100;

        [SerializeField]
        [Range(0, 1)]
        float _lerpSpeed = 0.1f;
        
        [ReadOnly]
        Transform _hand;

        Rigidbody _rb;
        
        #region MonoBehaviour
        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        void Update()
        {
            // exit, this item is not being held
            if (!Grabbed) return;

            MoveTowardsHand();
        }

        void OnCollisionEnter(Collision collision)
        {
            // exit, this item is not being held
            if (!Grabbed) return;

            // exit, force was not big enough
            if (collision.impulse.sqrMagnitude < _minBreakingForce * _minBreakingForce) return;

            Release();
        }
        #endregion

        public void Grab(Transform hand)
        {
            _hand = hand;

            _rb.useGravity = false;
        }

        // public void Rotate(float force)
        // {
        //     
        // }

        public void Release()
        {
            _hand = null;
            
            _rb.useGravity = true;
        }
        
        // Release, but also apply force along _hand.transform.forward
        public void Throw(float force=100)
        {
            Vector3 dir = _hand.transform.forward;
            Release();
            
            // apply force
            _rb.AddRelativeForce(dir * force);
        }

        void MoveTowardsHand()
        {
            Vector3 nPos = Vector3.Lerp(transform.position, _hand.position, _lerpSpeed);;
            _rb.MovePosition(nPos);
        }
    }
}