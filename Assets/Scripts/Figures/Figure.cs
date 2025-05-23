using UnityEngine;

namespace Figures
{
    public abstract class Figure : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer animal;
        
        public string Id { get; private set; }

        public void Setup(string id, Sprite animalSprite)
        {
            Id = id;
            animal.sprite = animalSprite;
        }
        
        public abstract Figure Clone();
    }
}
