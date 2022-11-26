using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class CustomerController : MonoBehaviour
    {
        [SerializeField] BarManager messyCustomerManager;
        NavMeshAgent agent;
        Seats seat;

        [Header("Customer Information")]
        [SerializeField] Ingredient favoriteIngredient;
        // TODO: sync poison tolerance with ingredient poison values
        [SerializeField] int poisonTolerance = 10;
        [SerializeField] bool useRandomPatience;
        [SerializeField] float patience;

        [Header("Art")]
        [SerializeField] Animation walkCycle;
        [SerializeField] Animation drink;
        [SerializeField] Animation tableBang;
        [SerializeField] Animation keelOver;

        private void Start()
        {
            if (useRandomPatience) {
                patience = Random.Range(patience-5 , patience+5);
            }
        }

        // for some reason, getting multiple log prints from the 1 line in 37, not really sure why
        // one is the correct seat, the other is null
        void Update()
        {
            Debug.Log("update " + seat);
            patience -= Time.deltaTime;
            if (patience <= 0) {
                walkAway();
            }

            int maybeBang = Random.Range(0, 10);
            if(maybeBang > 9) {
                bang();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Drink maybeDrink = collision.gameObject.GetComponent<Drink>();
            if(maybeDrink != null){
                receiveDrink(maybeDrink);
            }
        }

        public void setTarget(Transform newSeat)
        {
            seat = newSeat.GetComponentInParent<Seats>();
            agent = gameObject.GetComponent<NavMeshAgent>();
            agent.destination = newSeat.GetComponentInParent<Transform>().position;
        }

        void bang()
        {
            tableBang.Play();
        }

        void receiveDrink(Drink drink)
        {
            if(drink.getTotalPoisonValue() >= poisonTolerance) {
                die();
                return;
            }
            drink.score();
        }

        // TODO: Implement animations and figure out how to fix messyCustomerManager
        //
        // Also, for some reason there are multiple seat variables
        // same issue as before, look at the log statements, not really sure why its happening
        void walkAway()
        {
            // messyCustomerManager.removePatron();
            walkCycle.Play();
            Debug.Log(seat);
            if(seat != null){
                seat.emptySeat();
            }
            Destroy(gameObject);
        }

        void die()
        {
            messyCustomerManager.removePatron();
            keelOver.Play();
            seat.emptySeat();
        }
    }
}