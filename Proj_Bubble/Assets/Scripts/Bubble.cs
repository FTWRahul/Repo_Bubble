using System;
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

    public List<Vector3> NeighbourTransformsPositions = new List<Vector3>();
    
    public void Init(BubbleSO bubbleData)
    {
        _bubbleData = bubbleData;
        SetColor();
        SetNumber();
    }
    
    private void SetColor()
    {
        GetComponent<SpriteRenderer>().color = _bubbleData.BubbleColor;
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
       List<Vector2Int> neighbours = _currentNode.GetNeighbours();
       foreach (var n in neighbours)
       {
           Debug.Log("Neighbours are :  "+n.x + " : " + n.y);
       }
    }
}
