using System.Collections.Generic;
using Game.Tiles;
using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [Header("Grid")]
        [SerializeField] private List<BlankTile> _blankTilesLayout;
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        
        [Header("Level")]
        [SerializeField] private int _goalScore;
        [SerializeField] private int _maxMoves;
        [SerializeField] private int _levelNumber;
        [SerializeField] private TileSets _tileSets;

        public TileSets TileSets => _tileSets;

        public int LevelNumber => _levelNumber;

        public int MaxMoves => _maxMoves;

        public int GoalScore => _goalScore;

        public int Height => _height;

        public int Width => _width;

        public List<BlankTile> BlankTilesLayout => _blankTilesLayout;
    }

    public enum TileSets
    {
        Kingdom,
        Gem
    }
}