using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bubble : MonoBehaviour, IBubble
{
    private BubbleSO _bubbleData;

    public Color BubbleColor => _bubbleData.BubbleColor;
    public int BubbleNumber => _bubbleData.BubbleNumber;

    private HexNode _currentNode;
   
    public HexNode CurrentNode
    {
        get => _currentNode;
        set => _currentNode = value;
    }
   
    [SerializeField]
    private TextMeshProUGUI numberText;


    public void Init(BubbleSO bubbleData)
    {
        _bubbleData = bubbleData;
        SetColor();
        SetNumber();
    }
    
    private void SetColor()
    {
        GetComponent<SpriteRenderer>().material.color = _bubbleData.BubbleColor;
    }

    private void SetNumber()
    {
        numberText.text = _bubbleData.BubbleNumber.ToString();
    }

    public void Merge()
    {
        throw new System.NotImplementedException();
    }

    public void Pop()
    {
        throw new System.NotImplementedException();
    }

    public void GetNeighbour()
    {
        throw new System.NotImplementedException();
    }
}
