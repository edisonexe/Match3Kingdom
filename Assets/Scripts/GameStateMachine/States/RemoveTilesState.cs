using System;
using System.Collections.Generic;
using System.Threading;
using Animations;
using Audio;
using Game.GridSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Board;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Game.Utils;

namespace GameStateMachine.States
{
    public class RemoveTilesState : IState, IDisposable
    {
        private CancellationTokenSource _cts;
        private Grid _grid;
        private IStateSwitcher _stateSwitcher;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private ScoreCalculator  _scoreCalculator;
        private AudioManager _audioManager;
        private FXPool _fxPool;
        private GameBoard  _gameBoard;
        
        public RemoveTilesState(Grid grid, IStateSwitcher stateSwitcher, 
            IAnimation animation,  MatchFinder matchFinder, ScoreCalculator scoreCalculator,
            AudioManager audioManager, FXPool fxPool, GameBoard gameBoard)
        {
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _animation = animation;
            _matchFinder = matchFinder;
            _scoreCalculator = scoreCalculator;
            _audioManager = audioManager;
            _fxPool = fxPool;
            _gameBoard = gameBoard;
        }

        public async void Enter()
        {
            _cts = new CancellationTokenSource();
            var tilesSnapshot = _matchFinder.TilesToRemove.ToArray();
            _scoreCalculator.CalculateScoreToAdd(_matchFinder.CurrentMatchResult.MatchDirection);
            await RemoveTiles(tilesSnapshot, _grid);
            _stateSwitcher.SwitchState<RefillGridState>();
        }
        
        public void Exit()
        {
            _cts?.Cancel();
            _matchFinder.ClearTilesToRemove();

        }

        public void Dispose() => _cts?.Dispose();

        private async UniTask RemoveTiles(Tile[] tilesToRemove, Grid grid)
        {
            foreach (var tile in tilesToRemove)
            {
                _audioManager.PlayRemove();
                var pos = grid.WorldToGrid(tile.transform.position);
                _grid.SetValue(pos.x, pos.y, null);
                await _animation.HideTile(tile.gameObject);
                _fxPool.GetFXFromPool(tile.transform.position, _gameBoard.transform);

            }
        }
    }
}