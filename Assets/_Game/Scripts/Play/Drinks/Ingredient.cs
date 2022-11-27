using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Ingredient : MonoBehaviour
    {
        public int PoisonValue => poisonValue;
        [SerializeField] int poisonValue = 1;
    }
}