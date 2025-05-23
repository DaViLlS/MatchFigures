using UnityEngine;

namespace Figures
{
    public abstract class Figure : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer animal;
        
        public string Id { get; private set; }

        public void Setup(string id, GameObject shapePrefab, Sprite animalSprite, Color frameColor)
        {
            Id = id;
            animal.sprite = animalSprite;
            
            var shape = Instantiate(shapePrefab, transform);
            shape.GetComponent<SpriteRenderer>().color = frameColor;
        }
        
        public abstract Figure Clone();
    }
}
