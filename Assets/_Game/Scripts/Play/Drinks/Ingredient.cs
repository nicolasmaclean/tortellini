using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Ingredient : MonoBehaviour
    {

        [SerializeField] int poisonValue = 1;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public int getPoisonValue()
        {
            return poisonValue;
        }
    }
}