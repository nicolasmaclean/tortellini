using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gummi.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class BarManager: MonoBehaviour
    {
        [SerializeField] Pool customerPool;
        [SerializeField] Seat[] allSeats;
        int patronsInBar = 0;

        IEnumerator Start()
        {
            // try to maintain 5 customers in the shop at a time
            while (true)
            {
                if (patronsInBar < 5)
                {
                    spawnCustomer();
                }
                
                int randSecs = Random.Range(1, 10);
                yield return new WaitForSeconds(randSecs);
            }
        }

        void spawnCustomer()
        {
            // find seat for customer
            Seat seat = getEmptySeat();
            seat.occupySeat();
            
            // spawn customer
            GameObject go = customerPool.CheckOut();
            CustomerController customer = go.GetComponent<CustomerController>();
            
            go.transform.SetPositionAndRotation(transform.position, transform.rotation);
            customer.setTarget(this, seat);
            
            patronsInBar++;
        }

        public void removePatron()
        {
            patronsInBar--;
        }

        Seat getEmptySeat() => allSeats.FirstOrDefault(seat => !seat.IsOccupied);
    }
}