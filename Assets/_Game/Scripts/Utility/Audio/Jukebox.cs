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
        int length = 0;

        private void Start()
        {
            source = gameObject.GetComponentInParent<AudioSource>();
            length = clipList.Count;
        }

        private void OnCollisionEnter(Collision collision)
        {
            currentClip = wrapInt(currentClip+1);
            AudioClip newClip = clipList[currentClip];
            source.clip = newClip;
            source.Play();
        }

        int wrapInt(int numToWrap)
        {
            if(numToWrap == length) return 0;
            if(numToWrap > length) return numToWrap % length - 1;
            return numToWrap;
        }
    }
}