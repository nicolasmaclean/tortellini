using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class CustomerController : MonoBehaviour
    {
        // TODO: sync poison tolerance with ingredient poison values
        
        [Header("Customer Information")]
        [SerializeField] Ingredient favoriteIngredient;
        [SerializeField] int poisonTolerance = 10;
        [SerializeField] float patience;
        [SerializeField] bool useRandomPatience;

        [Header("Art")]
        [SerializeField] Animation walkCycle;
        [SerializeField] Animation drink;
        [SerializeField] Animation tableBang;
        [SerializeField] Animation keelOver;

        BarManager barManager;
        NavMeshAgent agent;
        Seat seat;
        
        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            
            if (useRandomPatience) {
                patience = Random.Range(patience-5 , patience+5);
            }
        }

        void Update()
        {
            if (patience <= 0) walkAway();

            int maybeBang = Random.Range(0, 10);
            if (maybeBang > 9) bang();
            
            patience -= Time.deltaTime;
        }

        void OnCollisionEnter(Collision collision)
        {
            Drink maybeDrink = collision.gameObject.GetComponent<Drink>();
            if(maybeDrink == null) return;
            
            receiveDrink(maybeDrink);
        }

        public void setTarget(BarManager barManager, Seat seat)
        {
            this.barManager = barManager;
            this.seat = seat;
            
            agent.destination = seat.transform.position;
        }

        void bang()
        {
            // tableBang.Play();
        }

        void receiveDrink(Drink drink)
        {
            if (drink.getTotalPoisonValue() >= poisonTolerance) {
                die();
                return;
            }
            
            drink.score();
        }

        // TODO: Implement animations
        void walkAway()
        {
            // walkCycle.Play();
            seat.emptySeat();
            barManager.removePatron();
            Destroy(gameObject);
        }

        void die()
        {
            // keelOver.Play();
            barManager.removePatron();
            seat.emptySeat();
        }
    }
}