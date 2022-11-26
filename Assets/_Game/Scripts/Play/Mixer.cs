using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Play
{
    public class Mixer : MonoBehaviour
    {
        [SerializeField] DrinkRecipeSO[] _recipes;
        [SerializeField] Transform _spawnDrinkPosition;

        private List<Ingredient> _ingredientsInMixer;

        private void Start()
        {
            _ingredientsInMixer = new List<Ingredient>();
        }
        public void Mix()
        {
            //if (_ingredientsInMixer.Count == 0) return;

            ///check all recipes
            ///if ingredients apply to any recipe
            ///instantiate drink from recipe
            ///empty ingredients after making drink

            DrinkRecipeSO drink = null;
            List<Ingredient> usedIngredients = new List<Ingredient>();

            //iterate through each recipe
            foreach (DrinkRecipeSO recipe in _recipes)
            {
                List<Ingredient.IngredientType> ingredientsFromRecipe = new List<Ingredient.IngredientType>(recipe.listOfIngredients);

                foreach (Ingredient ingredient in _ingredientsInMixer)
                {
                    if (ingredientsFromRecipe.Contains(ingredient._type))
                    {
                        ingredientsFromRecipe.Remove(ingredient._type);
                    }
                }

                if (ingredientsFromRecipe.Count == 0)
                {
                    drink = recipe;
                }
            }

            //spawn drink if ingredients exist
            if (drink != null)
            {
                Debug.Log("Make drink: " + drink.recipeName);
                Transform newDrink = Instantiate(drink.outputDrinkPrefab.transform, transform);
                newDrink.position = _spawnDrinkPosition.position;

                //one more loop to empty ingredients used in drink
                List<Ingredient.IngredientType> ingredientsFromRecipe = new List<Ingredient.IngredientType>(drink.listOfIngredients);
                foreach (Ingredient ingredient in _ingredientsInMixer)
                {
                    if (ingredientsFromRecipe.Contains(ingredient._type))
                    {
                        ingredientsFromRecipe.Remove(ingredient._type);
                        usedIngredients.Add(ingredient);
                    }

                    if(ingredientsFromRecipe.Count == 0) break;
                }

                foreach (Ingredient i in usedIngredients)
                {
                    i.EmptyIngredient();
                }
            }
        }
        private void OnTriggerStay(Collider other)
        {
            Ingredient ingredient = other.transform.GetComponentInParent<Ingredient>();
            if(ingredient != null)
            {
                if (ingredient._full)
                {
                    if (!_ingredientsInMixer.Contains(ingredient))
                    {
                        _ingredientsInMixer.Add(ingredient);
                        Mix();
                    }
                }
                else
                {
                    if (_ingredientsInMixer.Contains(ingredient))
                        _ingredientsInMixer.Remove(ingredient);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            Ingredient ingredient = other.transform.GetComponentInParent<Ingredient>();
            if(ingredient != null)
            {
                if (_ingredientsInMixer.Contains(ingredient))
                    _ingredientsInMixer.Remove(ingredient);
            }
        }
    }
}

