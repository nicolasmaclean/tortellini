using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Play
{
    public class FloatingLabel : MonoBehaviour
    {
        string _label;

        [SerializeField] TextMesh text;
        Camera cam;

        private void Awake()
        {
            if (GetComponentInParent<Ingredient>())
            {
                _label = GetComponentInParent<Ingredient>()._type.ToString();
            }
            else if (GetComponentInParent<Drink>())
            {
                //_label = GetComponentInParent<Drink>().
            }

            cam = Camera.main;
        }

        private void Update()
        {
            if(_label != null)
            {
                if(text.text != _label)
                {
                    text.text = _label;
                }

                transform.LookAt(cam.transform);
            }
        }
    }
}
