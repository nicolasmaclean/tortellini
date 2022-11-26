using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Seats : MonoBehaviour
    {
        public bool isOccupied = false;

        public void occupySeat()
        {
            isOccupied = true;
        }

        public void emptySeat()
        {
            isOccupied = false;
        }

        public bool isActuallyOccupied()
        {
            return isOccupied;
        }
    }
}