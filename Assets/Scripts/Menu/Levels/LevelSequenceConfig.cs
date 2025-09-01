using System;
using System.Collections.Generic;
using Level;
using UnityEngine;

namespace Menu.Levels
{
    [CreateAssetMenu(fileName = "LevelSequenceConfig", menuName = "Configs/LevelSequenceConfig")]
    public class LevelSequenceConfig : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levelSequence = new List<LevelConfig>();
        private int _minLevelCount = 5;
        
        public List<LevelConfig> LevelSequence => _levelSequence;
        
        
        private void OnValidate()
        {
            if (_levelSequence.Count != _minLevelCount)
            {
                throw new ArgumentOutOfRangeException("LevelSequenceConfig", "LevelSequenceConfig must be 5");
            }
        }
    }
}