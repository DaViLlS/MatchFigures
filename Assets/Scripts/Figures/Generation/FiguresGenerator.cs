using System.Collections.Generic;
using UnityEngine;

namespace Figures.Generation
{
    public class FiguresGenerator : MonoBehaviour
    {
        [SerializeField] private FiguresRegistry figuresRegistry;
        [SerializeField] private GenerationConfig generationConfig;
        [SerializeField] private FiguresViewConfig figuresViewConfig;
        [SerializeField] private Transform figuresRoot;
        [SerializeField] private float spawnOffset;

        private Dictionary<string, int> _figures;

        private void Awake()
        {
            _figures = new Dictionary<string, int>();
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
                
                _figures.Add(id, 0);
            }
        }

        private void GenerateFigures()
        {
            for (var i = 0; i < generationConfig.FiguresCount; i++)
            {
                var figurePrototype = figuresRegistry.GetRandomFigure();
                var figure = figurePrototype.Clone();
                figure.transform.position = figuresRoot.position;
                figuresRoot.position += Vector3.up * spawnOffset;
                figure.transform.position = new Vector3(figure.transform.position.x, figure.transform.position.y, 0);
            }
        }
    }
}