using Audio;
using Game.UI;
using UnityEngine;

namespace GameStateMachine.States
{
    public class DefeatState  : IState
    {
        private EndGamePanelView _endGamePanelView;

        public DefeatState(EndGamePanelView endGamePanelView)
        {
            _endGamePanelView = endGamePanelView;
        }

        public void Enter()
        {
            Debug.Log("Win State Enter");
            _endGamePanelView.ShowEndGamePanel(false);
        }

        public void Exit()
        {
        }
    }
}