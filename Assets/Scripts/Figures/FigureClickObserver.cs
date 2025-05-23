using System;
using System.Collections.Generic;
using Figures.Generation;
using UnityEngine;
using UnityEngine.Serialization;

namespace Figures
{
    public class FigureClickObserver : MonoBehaviour
    {
        public event Action<Figure> FigureClicked;
        
        [SerializeField] private FiguresManager figuresManager;
        
        public static FigureClickObserver Instance;
        
        private List<Figure> _figures;

        private void Awake()
        {
            Instance = this;
            _figures = new List<Figure>();
            figuresManager.OnFigureGenerated += FigureGenerated;
        }

        private void FigureGenerated(Figure figure)
        {
            figure.OnClick += OnClick;
            _figures.Add(figure);
        }

        private void OnClick(Figure figure)
        {
            FigureClicked?.Invoke(figure);
            _figures.Remove(figure);
        }

        private void OnDestroy()
        {
            foreach (var figure in _figures)
            {
                figure.OnClick -= OnClick;
            }
        }
    }
}