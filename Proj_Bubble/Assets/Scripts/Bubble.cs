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
    public string bubbleText => _bubbleData.DisplayText;

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
        numberText.text = _bubbleData.DisplayText;
    }

    public void Merge()
    {
        throw new System.NotImplementedException();
    }

    public void Pop()
    {
        throw new System.NotImplementedException();
    }

    [ContextMenu("TELL ME YOUR NEIGHBOURS NOW!")]
    public void GetNeighbour()
    {
       List<Vector2Int> neighbours = _currentNode.GetNeighbours();
       Debug.Log("For Node------> " +_currentNode.X+" : "+_currentNode.Y);
       foreach (var n in neighbours)
       {
           Debug.Log("Neighbours are :  "+n.x + " : " + n.y);
       }
    }
}
