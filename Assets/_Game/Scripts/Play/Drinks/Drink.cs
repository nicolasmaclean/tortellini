using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Drink : MonoBehaviour
    {
        int totalPoisonValue = 0;
        List<Ingredient> ingredients = new List<Ingredient>();

        // Start is called before the first frame update
        void Start()
        {
            setTotalPoisonValue();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void setTotalPoisonValue()
        {
            foreach (Ingredient ingredient in ingredients) {
                totalPoisonValue += ingredient.getPoisonValue();
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