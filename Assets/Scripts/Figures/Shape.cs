using System;
using UnityEngine;

namespace Figures
{
    public class Shape : MonoBehaviour
    {
        public event Action OnClick;
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        
        private void OnMouseUpAsButton()
        {
            OnClick?.Invoke();
        }
    }
}