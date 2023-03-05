using System;
using UnityEngine;

namespace Game.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        [SerializeField] private GridManager _gridManager;

        private void Awake()
        {
            Instance = this;
        }
        
        public void CreateLevel()
        {
            _gridManager.GenerateLevelGrid();
        }

        public void LoadLevel()
        {
            
        }
    }
}
