using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Play
{
    public class Ingredient : MonoBehaviour
    {
        public enum IngredientType
        {
            Lime_Juice, 
            Lemon_Juice,
            Ginger_Beer,
            Tonic_Water,
            Simple_Syrup,
            Egg_White,
            Bitters,
            Tomato_Juice,
            Orange_Juice,
            Coke
        }

        public IngredientType type;
    }
}
