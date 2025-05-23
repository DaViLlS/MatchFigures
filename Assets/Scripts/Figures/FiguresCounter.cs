using System;
using System.Collections.Generic;
using Figures.Generation;
using UnityEngine;

namespace Figures
{
    public class FiguresCounter : MonoBehaviour
    {
        public event Action<Figure> OnFiguresChanged;
        public event Action OnVictory;
        public event Action OnFailure;
        public event Action<string> OnFiguresMatched;
        
        [SerializeField] private FiguresManager figuresManager;
        [SerializeField] private int maxFiguresCount;

        private int _currentFiguresCount;
        private int _clickedFiguresCount;
        
        private Dictionary<string, int> _figuresCounter;
        private Dictionary<string, List<Figure>> _figures;

        private void Awake()
        {
            _figuresCounter = new Dictionary<string, int>();
            _figures = new Dictionary<string, List<Figure>>();
        }

        private void Start()
        {
            figuresManager.OnFigureGenerated += IncreaseFiguresCount;
            FigureClickObserver.Instance.FigureClicked += HandleFigureClick;
        }

        private void IncreaseFiguresCount(Figure figure)
        {
            _currentFiguresCount++;
        }

        private void HandleFigureClick(Figure figure)
        {
            _clickedFiguresCount++;

            if (!_figuresCounter.TryAdd(figure.Id, 1))
            {
                _figuresCounter[figure.Id]++;
            }

            if (!_figures.ContainsKey(figure.Id))
            {
                _figures[figure.Id] = new List<Figure>();
            }
            
            _figures[figure.Id].Add(figure);
            
            OnFiguresChanged?.Invoke(figure);

            if (_figuresCounter[figure.Id] == 3)
            {
                _figuresCounter[figure.Id] = 0;
                _clickedFiguresCount -= 3;
                OnFiguresMatched?.Invoke(figure.Id);
            }
            
            if (_clickedFiguresCount > 7)
            {
                OnFailure?.Invoke();
                return;
            }
            
            if (_currentFiguresCount <= 0)
            {
                OnVictory?.Invoke();
            }
        }
    }
}