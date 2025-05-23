using UnityEngine;

[CreateAssetMenu(fileName = "Generation Config", menuName = "Game/Generation Config")]
public class GenerationConfig : ScriptableObject
{
    [SerializeField] private int figuresCount;

    public int FiguresCount => figuresCount;
}