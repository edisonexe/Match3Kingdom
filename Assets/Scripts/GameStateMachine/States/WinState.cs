using UnityEngine;

namespace GameStateMachine.States
{
    public class WinState : IState
    {
        public void Enter()
        {
            Debug.Log("Entered WinState");
        }

        public void Exit()
        {
        }
    }
}