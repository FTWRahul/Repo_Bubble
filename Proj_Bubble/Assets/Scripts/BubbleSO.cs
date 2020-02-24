using UnityEngine;

[CreateAssetMenu(order = 1, fileName = "NewBubbleSO", menuName = "NewBubble")]
public class BubbleSO : ScriptableObject
{
    [SerializeField]
    private Color bubbleColor;
    [SerializeField]
    private int bubbleNumber;
    
    public Color BubbleColor => bubbleColor;
    
    public int BubbleNumber => bubbleNumber;
}
