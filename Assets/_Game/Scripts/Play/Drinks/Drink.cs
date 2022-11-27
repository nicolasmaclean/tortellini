using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Drink : MonoBehaviour
    {
        int totalPoisonValue = 0;
        List<Ingredient> ingredients = new List<Ingredient>();

        void Start()
        {
            setTotalPoisonValue();
        }

        void setTotalPoisonValue()
        {
            foreach (Ingredient ingredient in ingredients) {
                totalPoisonValue += ingredient.PoisonValue;
            }
        }
        public int getTotalPoisonValue()
        {
            return totalPoisonValue;
        }

        public void score()
        {

        }
    }
}