using Audio;
using Game.Score;
using Game.UI;
using UnityEngine;

namespace GameStateMachine.States
{
    public class WinState : IState
    {
        private EndGamePanelView _endGamePanelView;

        public WinState(EndGamePanelView endGamePanelView)
        {
            _endGamePanelView = endGamePanelView;
        }

        public void Enter()
        {
            Debug.Log("Win State Enter");
            _endGamePanelView.ShowEndGamePanel(true);
        }

        public void Exit()
        {}
    }
}