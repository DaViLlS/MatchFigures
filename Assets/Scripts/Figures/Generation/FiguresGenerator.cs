using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Figures.Generation
{
    public class FiguresGenerator : MonoBehaviour
    {
        private const int FiguresMultiply = 3;
        
        [SerializeField] private FiguresRegistry figuresRegistry;
        [SerializeField] private GenerationConfig generationConfig;
        [SerializeField] private FiguresViewConfig figuresViewConfig;
        [SerializeField] private FigureResourcesContainer figureResourcesContainer;
        [SerializeField] private Transform figuresRoot;
        [SerializeField] private float spawnOffset;

        private Dictionary<string, FigureData> _figuresData;
        private Dictionary<string, int> _figuresCount;

        private int _figuresCountToCreate;

        private void Awake()
        {
            _figuresData = new Dictionary<string, FigureData>();
            _figuresCount = new Dictionary<string, int>();
        }

        private void Start()
        {
            InitFigures();
            GenerateFigures();
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

        private void GenerateFigures()
        {
            for (var i = 0; i < generationConfig.ApproximateFiguresCount; i++)
            {
                var id = GetRandomFigureId();
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
                figure.Setup(id, animalSprite);
            }
        }

        private string GetRandomFigureId()
        {
            var randomIndex = Random.Range(0, _figuresData.Count);
            return _figuresData.Keys.ToList()[randomIndex];
        }
    }
}