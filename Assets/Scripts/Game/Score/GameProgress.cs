using System;
using UnityEngine;

namespace Game.Score
{
    public class GameProgress
    {
        public event Action OnScoreChanged;
        public event Action OnMove;
        public int Score {get; private set;}
        public int GoalScore {get; private set;}
        public int Moves { get; private set; }
        
        public void LoadLevelConfig(int goalScore, int moves)
        {
            Score = 0;
            GoalScore = goalScore;
            Moves = moves;
        }

        public void AddScore(int value)
        {
            if(value <= 0)
                throw new ArgumentOutOfRangeException("value", "Score cannot be less than or equal to 0");
            Score += value;
            OnScoreChanged?.Invoke();
        }

        public bool CheckGoalScore()
        {
            return Score >= GoalScore;
        }

        public void SpendMove()
        {
            Moves--;
            Debug.Log(Moves + " to lose");
            OnMove?.Invoke();
        }
    }
}