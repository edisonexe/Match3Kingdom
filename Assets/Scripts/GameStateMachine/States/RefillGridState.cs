using System;
using UnityEngine;

namespace GameStateMachine.States
{
    public class RefillGridState : IState, IDisposable
    {
        public void Enter()
        {
            Debug.Log("Refill Grid Enter");
        }

        public void Exit()
        {

        }

        public void Dispose()
        {

        }
    }
}