using UnityEngine;

namespace GameStateMachine.States
{
    public class LoseState  : IState
    {
        public void Enter()
        {
            Debug.Log("Entered LoseState");
        }

        public void Exit()
        {
        }
    }
}