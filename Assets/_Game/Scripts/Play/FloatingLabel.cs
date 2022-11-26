using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Play
{
    public class FloatingLabel : MonoBehaviour
    {
        string _label;

        [SerializeField] TextMesh text;

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
        }

        private void Update()
        {
            if(_label != null)
            {
                //if()
            }
        }
    }
}
