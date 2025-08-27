using Game.Board;
using UnityEngine;
using GameStateMachine;

namespace EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] GameBoard  _gameBoard;
        private StateMachine _stateMachine;
        private void Start()
        {
            _stateMachine = new StateMachine(_gameBoard);
        }
    }
}