using Animations;
using Audio;
using Data;
using Game.Board;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Game.UI;
using Game.Utils;
using GameStateMachine;
using Level;
using ResourcesLoading;
using SceneLoader;
using VContainer.Unity;
using Grid = Game.GridSystem.Grid;

namespace Game.EntryPoint
{
    public class GameEntryPoint : IInitializable
    {
        private BackgroundTilesSetup _backgroundTilesSetup;
        private LevelConfig _levelConfig;
        private ScoreCalculator _scoreCalculator;
        private BlankTilesSetup _blankTilesSetup;
        private StateMachine _stateMachine;
        private GameProgress _gameProgress;
        private Grid _grid;
        private MatchFinder _matchFinder;
        private GameBoard  _gameBoard;
        private GameDebug _gameDebug;
        private IAnimation _animation;
        private TilePool _tilePool;
        private GameData _gameData;
        private AudioManager _audioManager;
        private IAsyncSceneLoading _sceneLoading;
        private EndGamePanelView  _endGamePanelView;
        private GameResourcesLoader _resourcesLoader;
        private SetupCamera _setupCamera;
        private FXPool _fxPool;

        private bool _isDebugging;
        
        public GameEntryPoint(ScoreCalculator scoreCalculator, BlankTilesSetup blankTilesSetup,
            GameProgress gameProgress, Grid grid, MatchFinder matchFinder, 
            GameBoard gameBoard, GameDebug gameDebug, IAnimation animation, TilePool tilePool, GameData gameData, 
            AudioManager audioManager, IAsyncSceneLoading sceneLoading, EndGamePanelView endGamePanelView, 
            GameResourcesLoader resourcesLoader, SetupCamera setupCamera, BackgroundTilesSetup backgroundTilesSetup,
            FXPool fxPool)
        {
            _scoreCalculator = scoreCalculator;
            _blankTilesSetup = blankTilesSetup;
            _gameProgress = gameProgress;
            _grid = grid;
            _fxPool  = fxPool;
            _matchFinder = matchFinder;
            _gameBoard = gameBoard;
            _gameDebug = gameDebug;
            _animation = animation;
            _tilePool = tilePool;
            _gameData = gameData;
            _audioManager = audioManager;
            _sceneLoading = sceneLoading;
            _endGamePanelView = endGamePanelView;
            _resourcesLoader = resourcesLoader;
            _setupCamera = setupCamera;
            _backgroundTilesSetup = backgroundTilesSetup;
        }

        public async void Initialize()
        {
            _levelConfig = _gameData.CurrentLevel;
            if (_isDebugging)
                _gameDebug.ShowDebug(_gameBoard.transform);
            _grid.SetupGrid(_levelConfig.Width, _levelConfig.Height);
            _gameProgress.LoadLevelConfig(_levelConfig.GoalScore, _levelConfig.MaxMoves);
            await _resourcesLoader.Load();
            _setupCamera.SetCamera(_grid.Width, _grid.Height, false);
            _blankTilesSetup.SetupBlanks(_levelConfig);
            _stateMachine = new StateMachine(_gameBoard, _grid, _animation, _matchFinder, _tilePool, 
                _gameProgress, _scoreCalculator,  _audioManager, _endGamePanelView, 
                _backgroundTilesSetup, _blankTilesSetup, _levelConfig, _fxPool);
            _sceneLoading.LoadingIsDone(true);
        }
    }
}