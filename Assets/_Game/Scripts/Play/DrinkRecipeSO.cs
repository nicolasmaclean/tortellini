using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Play
{
    [CreateAssetMenu(menuName = "DrinkRecipe")]
    public class DrinkRecipeSO : ScriptableObject
    {
        public string recipeName;
        public GameObject outputDrinkPrefab;
        public List<Ingredient.IngredientType> listOfIngredients;
    }
}
