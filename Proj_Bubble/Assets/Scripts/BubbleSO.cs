using UnityEngine;

[CreateAssetMenu(order = 1, fileName = "NewBubbleSO", menuName = "NewBubble")]
public class BubbleSO : ScriptableObject
{
    [SerializeField]
    private Color bubbleColor;
    [SerializeField]
    private int bubbleNumber;
    [SerializeField]
    private string displayText;
    
    public Color BubbleColor => bubbleColor;
    
    public int BubbleNumber => bubbleNumber;

    public string DisplayText => displayText;
}
