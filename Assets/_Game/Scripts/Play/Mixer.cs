using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Play
{
    public class Mixer : MonoBehaviour
    {
        /// When ingredients are placed in mixer
        /// check if ingredients complete a recipe
        /// if they do then make new drink
        /// 

        //[SerializeField] IngredientScriptableObject testIngredient;
        //[SerializeField] BoxCollider mixAreaCollider;
        [SerializeField] DrinkRecipeSO[] _recipes;
        [SerializeField] Transform _spawnDrinkPosition;

        [SerializeField] private List<Ingredient> _ingredientsInMixer;

        bool mixing = false;

        private void Start()
        {
            _ingredientsInMixer = new List<Ingredient>();
        }
        private void Update()
        {
            //Mix();
        }
        public void Mix()
        {
            //if (_ingredientsToCombine.Count == 0) return;

            //Debug.Log("Mix");
            //Debug.Log("ingredients in mixer: " + ingredientsToCombine.Count);

            ///check all recipes
            ///if ingredients apply to any recipe
            ///instantiate drink from recipe
            ///empty ingredients after making drink

            DrinkRecipeSO drink = null;
            List<Ingredient> usedIngredients = new List<Ingredient>();

            #region New
            //iterate through each recipe
            /*foreach(DrinkRecipeSO recipe in _recipes)
            {
                List<Ingredient.IngredientType> ingredientsForRecipe = new List<Ingredient.IngredientType>(recipe.listOfIngredients);

                foreach(Ingredient ingredient in _ingredientsToCombine)
                {
                    if(ingredientsForRecipe.Count > 0)
                    {
                        if (ingredientsForRecipe.Contains(ingredient._type))
                        {
                            ingredientsForRecipe.Remove(ingredient._type);
                            usedIngredients.Add(ingredient);
                            //Debug.Log(ingredientsForRecipe.Count);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                
                Debug.Log("ingredientsForRecipe.Count = " + ingredientsForRecipe.Count);

                if (ingredientsForRecipe.Count == 0)
                {
                    drink = recipe;
                    Debug.LogWarning("Make Drink");
                }

                Debug.Log("usedIngredients.Count = " + usedIngredients.Count);

            }*/
            #endregion
            #region Old
            foreach (DrinkRecipeSO recipe in _recipes)
            {
                List<Ingredient.IngredientType> ingredientsFromRecipe = new List<Ingredient.IngredientType>(recipe.listOfIngredients);

                Debug.LogWarning("[1][before]ingredientsForRecipe.Count = " + ingredientsFromRecipe.Count);

                foreach (Ingredient ingredient in _ingredientsInMixer)
                {
                    Debug.Log("[2]ingredient._type: " + ingredient._type);
                    if (ingredientsFromRecipe.Contains(ingredient._type))
                    {
                        Debug.Log("[3]ingredientsFromRecipe.Contains(ingredient._type)");

                        ingredientsFromRecipe.Remove(ingredient._type);

                        if (!usedIngredients.Contains(ingredient))
                            usedIngredients.Add(ingredient);

                        /*if (ingredientsFromRecipe.Count > 0)
                        {
                            
                        }
                        else
                            break;*/
                    }
                }

                Debug.LogWarning("[4][after]ingredientsForRecipe.Count = " + ingredientsFromRecipe.Count);

                if (ingredientsFromRecipe.Count == 0)
                {
                    Debug.Log("[5]ingredientsFromRecipe.Count == 0");
                    drink = recipe;
                }
            }
            #endregion

            //spawn drink if ingredients exist
            if (drink != null)
            {
                Debug.Log("Make drink: " + drink.recipeName);
                Transform newDrink = Instantiate(drink.outputDrinkPrefab.transform, null);
                newDrink.position = _spawnDrinkPosition.position;

                //change used ingredients to empty
                foreach (Ingredient ing in usedIngredients)
                {
                    ing.EmptyIngredient();
                }

                drink = null;
            }

            

            #region Trash
            /*Collider[] colliderArray = Physics.OverlapBox(transform.position + mixAreaCollider.center, 
                mixAreaCollider.size, 
                mixAreaCollider.transform.rotation);

            foreach (Collider collider in colliderArray)
            {
                //Debug.LogWarning(collider.name);
                if (collider.transform.GetComponentInParent<Ingredient>())
                {
                    Ingredient ingredient = collider.GetComponentInParent<Ingredient>();
                    Debug.Log(ingredient.type);
                }

            }*/
            #endregion
        }
        private void OnTriggerEnter(Collider other)
        {
            Ingredient ingredient = other.transform.GetComponentInParent<Ingredient>();
            if (ingredient != null)
            {
                //Mix();
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

                /*if (mixing == false)
                    {
                        mixing = true;
                        Mix();
                    }*/
            }
        }
        private void OnTriggerExit(Collider other)
        {
            Ingredient ingredient = other.transform.GetComponentInParent<Ingredient>();
            if(ingredient != null)
            {
                if (_ingredientsInMixer.Contains(ingredient))
                    _ingredientsInMixer.Remove(ingredient);

                /*if (mixing == true)
                {
                    mixing = false;
                }*/
            }
        }
    }
}

