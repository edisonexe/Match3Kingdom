using Audio;
using UnityEngine;

namespace GameStateMachine.States
{
    public class DefeatState  : IState
    {
        private AudioManager _audioManager;

        public DefeatState(AudioManager audioManager) => _audioManager = audioManager;

        public void Enter()
        {
            Debug.Log("Entered LoseState");
            _audioManager.PlayDefeat();
        }

        public void Exit()
        {
        }
    }
}