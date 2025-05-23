using System;
using System.Collections.Generic;
using System.Linq;
using Figures.FigureStructure;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Figures.Generation
{
    public class FiguresManager : MonoBehaviour
    {
        public event Action<Figure> OnFigureGenerated;
        
        private const int FiguresMultiply = 3;
        
        [SerializeField] private FiguresRegistry figuresRegistry;
        [SerializeField] private GenerationConfig generationConfig;
        [SerializeField] private FiguresViewConfig figuresViewConfig;
        [SerializeField] private FigureResourcesContainer figureResourcesContainer;
        [SerializeField] private FiguresCounter figuresCounter;
        [SerializeField] private Transform figuresRoot;
        [SerializeField] private float spawnOffset;

        private Dictionary<string, FigureData> _figuresData;
        private Dictionary<string, int> _figuresCount;
        private List<Figure> _figures;

        private int _figuresCountToCreate;
        private bool _figuresSetupDone;

        private void Awake()
        {
            _figuresData = new Dictionary<string, FigureData>();
            _figuresCount = new Dictionary<string, int>();
            _figures = new List<Figure>();
        }

        private void Start()
        {
            InitFigures();
            GenerateFigures();

            figuresCounter.OnFailure += DestroyFigures;
        }

        private void InitFigures()
        {
            foreach (var figureData in figuresViewConfig.Figures)
            {
                var id = $"{figureData.shapeType}_{figureData.animalType}_{figureData.colorType}";
                
                _figuresData.Add(id, figureData);
                _figuresCount.Add(id, 0);
            }

            CalculateAccurateFiguresCount();
            SetupIndividualFiguresCount();
        }
        
        private void CalculateAccurateFiguresCount()
        {
            var figuresViewLength = figuresViewConfig.Figures.Length;
            _figuresCountToCreate = figuresViewLength * FiguresMultiply;

            if (_figuresCountToCreate > generationConfig.ApproximateFiguresCount)
            {
                var difference = _figuresCountToCreate - generationConfig.ApproximateFiguresCount;
                figuresViewLength -= (difference % FiguresMultiply) * FiguresMultiply;
                _figuresCountToCreate = figuresViewLength * FiguresMultiply;
            }
            else if (_figuresCountToCreate < generationConfig.ApproximateFiguresCount)
            {
                var difference = generationConfig.ApproximateFiguresCount - _figuresCountToCreate;
                figuresViewLength += (difference % FiguresMultiply) * FiguresMultiply;
                _figuresCountToCreate = figuresViewLength * FiguresMultiply;
            }
        }

        private void SetupIndividualFiguresCount()
        {
            var count = 0;
            var duplicateFiguresCount = _figuresCount.ToDictionary(fig => fig.Key, fig => fig.Value);
            
            foreach (var figureId in duplicateFiguresCount.Keys)
            {
                _figuresCount[figureId] = 3;
                count += 3;
            }

            while (count < _figuresCountToCreate)
            {
                var id = GetRandomFigureId();
                _figuresCount[id] = 3;
                count += 3;
            }
        }

        private void GenerateFigures()
        {
            for (var i = 0; i < _figuresCountToCreate; i++)
            {
                var id = GetRandomFigureId();

                if (_figuresCount[id] == 0)
                {
                    id = _figuresCount.Keys.First(x => _figuresCount[x] > 0);
                }
                
                _figuresCount[id]--;
                
                var figureData = _figuresData[id];
                var animalSprite = figureResourcesContainer.GetAnimalSprite(figureData.animalType);
                var shapePrefab = figureResourcesContainer.GetFigureShape(figureData.shapeType);
                
                var shape = Instantiate(shapePrefab, transform);
                shape.GetComponent<SpriteRenderer>().color = figureData.colorType;
                shape.transform.position = figuresRoot.position;
                figuresRoot.position += Vector3.up * spawnOffset;
                shape.transform.position = new Vector3(shape.transform.position.x, shape.transform.position.y, 0);
                
                var figurePrototype = figuresRegistry.GetRandomFigure();
                var figure = figurePrototype.Clone();
                figure.transform.SetParent(shape.transform);
                figure.transform.localPosition = Vector3.zero;
                figure.Setup(id, animalSprite,  shape.GetComponent<Shape>());
                figure.OnClick += RemoveFigureFromList;
                
                _figures.Add(figure);
                OnFigureGenerated?.Invoke(figure);
            }
        }

        private void RemoveFigureFromList(Figure figure)
        {
            _figures.Remove(figure);
        }

        private string GetRandomFigureId()
        {
            var randomIndex = Random.Range(0, _figuresData.Count);
            return _figuresData.Keys.ToList()[randomIndex];
        }
        
        private void DestroyFigures()
        {
            foreach (var figure in _figures)
            {
                figure.DestroyFigure();
            }
            
            _figures.Clear();
        }

        private void OnDestroy()
        {
            figuresCounter.OnFailure -= DestroyFigures;
        }
    }
}