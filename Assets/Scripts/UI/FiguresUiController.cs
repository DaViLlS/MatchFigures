using System;
using System.Collections.Generic;
using Figures;
using Figures.Ui;
using UnityEngine;

namespace UI
{
    public class FiguresUiController : MonoBehaviour
    {
        [SerializeField] private FiguresCounter figuresCounter;
        [SerializeField] private GameObject failureWindow;
        
        private Dictionary<FigureUiView, string> _figuresUiViews;
        private List<FigureUiView> _figuresToRemove;

        private void Awake()
        {
            _figuresToRemove = new List<FigureUiView>();
            _figuresUiViews = new Dictionary<FigureUiView, string>();
            figuresCounter.OnFiguresChanged += FiguresChanged;
            figuresCounter.OnFiguresMatched += FiguresMatched;
            figuresCounter.OnFailure += Failure;
        }

        private void FiguresChanged(Figure figure)
        {
            figure.FigureUiView.gameObject.SetActive(true);
            var screenPosition = Camera.main.WorldToScreenPoint(figure.transform.position);
            figure.FigureUiView.transform.position = screenPosition;
            _figuresUiViews.Add(figure.FigureUiView, figure.Id);
        }
        
        private void FiguresMatched(string figureId)
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
        
        private void Failure()
        {
            ClearUiViews();
            failureWindow.SetActive(true);
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