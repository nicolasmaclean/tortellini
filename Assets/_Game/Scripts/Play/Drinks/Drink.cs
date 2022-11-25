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
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public int getTotalPoisonValue()
        {
            foreach(Ingredient ingredient in ingredients) {
                totalPoisonValue += ingredient.getPoisonValue();
            }
            return totalPoisonValue;
        }

        public void score()
        {

        }
    }
}
