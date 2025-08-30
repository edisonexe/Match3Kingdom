using System;
using System.Collections.Generic;
using System.Threading;
using Animations;
using Game.GridSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;

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
        public RemoveTilesState(Grid grid, IStateSwitcher stateSwitcher, 
            IAnimation animation,  MatchFinder matchFinder, ScoreCalculator scoreCalculator)
        {
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _animation = animation;
            _matchFinder = matchFinder;
            _scoreCalculator = scoreCalculator;
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
                // play sound
                var pos = grid.WorldToGrid(tile.transform.position);
                _grid.SetValue(pos.x, pos.y, null);
                await _animation.HideTile(tile.gameObject);
                // FX deleting tiles
                
            }
        }
    }
}