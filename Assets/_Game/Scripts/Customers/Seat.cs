using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Seat : MonoBehaviour
    {
        public bool IsOccupied { get; private set; }

        public void occupySeat()
        {
            IsOccupied = true;
        }

        public void emptySeat()
        {
            IsOccupied = false;
        }
    }
}