using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class BarManager: MonoBehaviour
    {
        [SerializeField] GameObject customerPrefab;
        [SerializeField] Seats[] allSeats;
        [SerializeField] Seats work;
        int patronsInBar = 0;

        private void Start()
        {
            StartCoroutine(ImprovedUpdate());
        }

        IEnumerator ImprovedUpdate(){
            if (patronsInBar < 5) {
                spawnCustomer();
            }
            int randSecs = Random.Range(1, 10);
            yield return new WaitForSeconds(randSecs);
            StartCoroutine(ImprovedUpdate());
        }

        void spawnCustomer()
        {
            patronsInBar++;
            GameObject customer = Instantiate(customerPrefab, gameObject.GetComponent<Transform>());
            Seats seat = getEmptySeat();
            seat.occupySeat();
            Transform trans = seat.GetComponentInParent<Transform>();
            customer.GetComponent<CustomerController>().setTarget(trans);
        }

        public void removePatron()
        {
            patronsInBar--;
        }

        Seats getEmptySeat()
        {
            for(int i = 0; i < allSeats.Length; i++) {
                if(!allSeats[i].isActuallyOccupied()){
                    return allSeats[i];
                }
            }
            return null;
        }
    }
}