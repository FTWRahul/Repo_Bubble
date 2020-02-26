using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BubbleTweener))]
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
       
       //Debug.Log("For Node------> " +_currentNode.X+" : "+_currentNode.Y);
       foreach (var neighbour in neighbours)
       {
           //Debug.Log("Neighbours are :  "+neighbour.x + " : " + neighbour.y);
           
       }
    }

    public Vector2Int GetNearestAvailableNeighbour(Vector2 from)
    {
        List<Vector2Int> neighbours = _currentNode.GetNeighbours();
        Debug.Log(_currentNode.X + "  " +_currentNode.Y);
        Vector2Int nearest = new Vector2Int();
        float distanceThreshold = Mathf.Infinity;
        foreach (Vector2Int neighbour in neighbours)
        {
           float current = Vector3.Distance(BubbleManager.Instance.WorldNodePos(neighbour.x, neighbour.y), transform.position);
           if (current < distanceThreshold)
           {
               nearest = neighbour;
           }
        }
        Debug.Log("Nearest Neighbour is :" + nearest);
        return nearest;
    }
}
