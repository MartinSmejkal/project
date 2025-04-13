using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    /*
         * Class representing single tile/Box on the boeard/Field,
         * stores the owner and timer state if needed.
         */
    class Box
    {
        /*
         * Constructor for Box (tile on the gameboard), 
         * sets default owner to 'Box.empty',LockTimer to '0' and timerMAX to '-1'.
         * For more information see LockTimer.
         */
        public Box(sbyte timerMax)
        {
            if (timerMax < -1)
            {
                timerMax = -1;
            }
            Owner = State.empty;
            timerMAX = timerMax;
            LockTimer = 0;
        }

        private Box()
        { /*
            Owner = State.empty;
            timerMAX = -1;
            LockTimer = 0;
            */
        }

        /*
         * Enumeration of possible states/owners of a box.
         */
        public enum State
        {
            circle,
            cross,
            empty

        }

        public State Owner { get; private set; }

        /*
         * Function to set/change the owner of the box, change is allowed only when LockTimer is '0', 
         * parameter is enum Box.State, new owner of this Box instance,
         * this schould be checked in Field to highlight possible moves,
         * sets the LockTimer to timerMAX, by default to '-1' to block overwriting.
        */
        public bool SetOwner(State p)
        {
            if (LockTimer == 0)
            {
                Owner = p;
                ResetTimer();
                return true;
            }
            return false;

        }

        /*
         * Alternative settings to allow changing previously set Owner (overwritng).
         */

        /*
         * Timer will reset to this number, default is '-1'.
         */
        private sbyte timerMAX;
        /*
         * Number of rounds this Box will block changing its owner. 
         * negative value does not allow overwritng, Box has recieved it`s owner,
         * positive value represents number of round to be played to allow new change,
         * default is '0' to allow at least single change of owner.
         */
        public sbyte LockTimer { get; private set; }

        /*
         * After a round is played decremens the LockTimer by '1', until it reaches '0'.
         */
        public void DecrementLock()
        {
            if (LockTimer > 0)
            {
                LockTimer--;
            }
        }

        /*
         * Function reseting the LockTimer to timerMAX, 
         * called during change of owner.
         */
        private void ResetTimer()
        {
            LockTimer = timerMAX;

        }


    }
}
