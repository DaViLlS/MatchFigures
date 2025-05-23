using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Figures;
using Figures.Ui;
using UnityEngine;

namespace UI
{
    public class FiguresUiController : MonoBehaviour
    {
        [SerializeField] private Transform[] figurePositions;
        [SerializeField] private FiguresCounter figuresCounter;
        [SerializeField] private GameObject failureWindow;
        [SerializeField] private GameObject victoryWindow;
        
        private Dictionary<FigureUiView, string> _figuresUiViews;
        private Dictionary<Transform, FigureUiView> _positions;
        private List<FigureUiView> _figuresToRemove;
        private List<Tween> _moveTweens;

        private void Awake()
        {
            _figuresToRemove = new List<FigureUiView>();
            _figuresUiViews = new Dictionary<FigureUiView, string>();
            _positions = new Dictionary<Transform, FigureUiView>();
            _moveTweens = new List<Tween>();

            foreach (var position in figurePositions)
            {
                _positions.Add(position, null);
            }
            
            figuresCounter.OnFiguresChanged += FiguresChanged;
            figuresCounter.OnFiguresMatched += FiguresMatched;
            figuresCounter.OnVictory += ShowVictoryWindow;
            figuresCounter.OnFailure += ShowFailureWindow;
        }

        private void FiguresChanged(Figure figure)
        {
            var figureUiView = figure.FigureUiView;
            figureUiView.gameObject.SetActive(true);
            var screenPosition = Camera.main.WorldToScreenPoint(figure.transform.position);
            figureUiView.transform.position = screenPosition;
            _figuresUiViews.Add(figureUiView, figure.Id);
            
            var freePosition = GetAndOccupyFreePosition(figureUiView);
            
            if (freePosition == null)
                return;
            
            _moveTweens.Add(figureUiView.MoveTo(freePosition.transform.position));
        }

        private Transform GetAndOccupyFreePosition(FigureUiView figureUiView)
        {
            var freePosition = _positions.FirstOrDefault(x => x.Value == null).Key;
            
            if (freePosition == null)
                return null;
            
            _positions[freePosition] = figureUiView;
            
            return freePosition;
        }
        
        private void FiguresMatched(string figureId)
        {
            var lastTween = _moveTweens.Last();
            
            if (lastTween.IsActive())
                _moveTweens.Last().OnComplete(() => RemoveFigures(figureId));
        }

        private void RemoveFigures(string figureId)
        {
            foreach (var keyValue in _figuresUiViews)
            {
                if (keyValue.Value == figureId)
                { 
                    _figuresToRemove.Add(keyValue.Key);
                    Destroy(keyValue.Key.gameObject);
                }
            }

            foreach (var figureUiView in _figuresToRemove)
            {
                _figuresUiViews.Remove(figureUiView);
            }
            
            _figuresToRemove.Clear();
        }
        
        private void ShowVictoryWindow()
        {
            UnsubscribeFromChanges();
            ClearUiViews();
            victoryWindow.SetActive(true);
        }
        
        private void ShowFailureWindow()
        {
            UnsubscribeFromChanges();
            ClearUiViews();
            failureWindow.SetActive(true);
        }

        private void UnsubscribeFromChanges()
        {
            figuresCounter.OnFiguresChanged -= FiguresChanged;
            figuresCounter.OnFiguresMatched -= FiguresMatched;
        }

        private void ClearUiViews()
        {
            foreach (var keyValue in _figuresUiViews)
            {
                _figuresToRemove.Add(keyValue.Key);
                Destroy(keyValue.Key.gameObject);
            }

            foreach (var figureUiView in _figuresToRemove)
            {
                _figuresUiViews.Remove(figureUiView);
            }
            
            _figuresToRemove.Clear();
        }
    }
}