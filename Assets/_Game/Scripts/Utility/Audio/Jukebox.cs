using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class Jukebox : MonoBehaviour
    {
        public List<AudioClip> clipList = new List<AudioClip>();
        AudioSource source;
        int currentClip = 0;

        private void Start()
        {
            source = gameObject.GetComponentInParent<AudioSource>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            currentClip = (currentClip + 1) % clipList.Count;
            
            AudioClip newClip = clipList[currentClip];
            source.clip = newClip;
            source.Play();
        }
    }
}