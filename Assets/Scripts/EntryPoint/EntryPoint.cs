using Animations;
using Game.Board;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using UnityEngine;
using GameStateMachine;
using Level;
using VContainer;
using Grid = Game.GridSystem.Grid;

namespace EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] GameBoard  _gameBoard;
        private StateMachine _stateMachine;
        private Grid _grid;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private GameProgress _gameProgress;
        private ScoreCalculator _scoreCalculator;
        private void Start()
        {
            _stateMachine = new StateMachine(_gameBoard, _grid, _animation, 
                _matchFinder, _tilePool, _gameProgress, _scoreCalculator);
            _gameProgress.LoadLevelConfig(_gameBoard.LevelConfig.GoalScore, _gameBoard.LevelConfig.MaxMoves);
        }

        [Inject] private void Construct(Grid grid, IAnimation animation, 
            MatchFinder matchFinder,  TilePool tilePool, GameProgress gameProgress,
            ScoreCalculator scoreCalculator)
        {
            _grid  = grid;
            _animation = animation;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
            _gameProgress = gameProgress;
            _scoreCalculator = scoreCalculator;
        }
    }
}