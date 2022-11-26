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
        [SerializeField] BoxCollider mixAreaCollider;
        [SerializeField] DrinkRecipeSO[] recipes;

        private void Start()
        {

        }
        private void Update()
        {
            Mix();
        }
        public void Mix()
        {
            Debug.Log("Mix");
            Collider[] colliderArray = Physics.OverlapBox(transform.position + mixAreaCollider.center, 
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

            }
        }
    }
}

