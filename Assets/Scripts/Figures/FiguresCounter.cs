using System;
using System.Collections.Generic;
using UnityEngine;

namespace Figures
{
    public class FiguresCounter : MonoBehaviour
    {
        public event Action OnFiguresChanged;
        public event Action OnFailure;
        public event Action OnFiguresMatched;
        
        [SerializeField] private int maxFiguresCount;

        private int _currentFiguresCount;
        
        private Dictionary<string, int> _figuresCounter;
        private Dictionary<string, List<Figure>> _figures;

        private void Awake()
        {
            _figuresCounter = new Dictionary<string, int>();
            _figures = new Dictionary<string, List<Figure>>();
        }

        private void HandleFigureClick(Figure figure)
        {
            _currentFiguresCount++;
            _figuresCounter[figure.Id]++;

            if (!_figures.ContainsKey(figure.Id))
            {
                _figures[figure.Id] = new List<Figure>();
            }
            
            _figures[figure.Id].Add(figure);
            
            OnFiguresChanged?.Invoke();

            if (_figuresCounter[figure.Id] == 3)
            {
                _figuresCounter[figure.Id] = 0;
                _currentFiguresCount -= 3;
                OnFiguresMatched?.Invoke();
            }

            if (_currentFiguresCount >= 7)
            {
                Debug.Log("Поражение");
                OnFailure?.Invoke();
            }
        }
    }
}