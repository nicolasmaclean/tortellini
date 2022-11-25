using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CustomerController : MonoBehaviour
    {
        [Header("Customer Information")]
        [SerializeField] Ingredient favoriteIngredient;
        // TODO: sync poison tolerance with ingredient poison values
        [SerializeField] int poisonTolerance = 10;
        // TODO: ensure this works and makes sense; should be too long rather than too short
        [SerializeField] float patience = 30;

        [Header("Art")]
        [SerializeField] Animation walkCycle;
        [SerializeField] Animation drink;
        [SerializeField] Animation tableBang;
        [SerializeField] Animation keelOver;

        private int seatNumber;

        void Update()
        {
            patience -= Time.deltaTime;
            if(patience <= 0){
                walkAway();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Drink maybeDrink = collision.gameObject.GetComponent<Drink>();
            if (maybeDrink != null){
                receiveDrink(maybeDrink);
            }
        }

        void receiveDrink(Drink drink)
        {
            if (drink.getTotalPoisonValue() >= poisonTolerance) {
                die();
                return;
            }
            drink.score();
        }

        void walkAway()
        {
            walkCycle.Play();
            seatNumber = 0;
        }

        void die()
        {
            keelOver.Play();
        }
    }
}