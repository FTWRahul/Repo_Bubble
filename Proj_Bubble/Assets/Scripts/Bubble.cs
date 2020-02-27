using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BubbleTweener))]
public class Bubble : MonoBehaviour, IBubble
{
    private BubbleSO _bubbleData;
    
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

    [ContextMenu("Try Merging for me will ya?")]
    public void StartMerge()
    {
        MergeHandler mergeHandler = new MergeHandler(this);
        mergeHandler.StartCheck();
    }

    public void Merge(IBubble bubble)
    {
        ClearNode();
        GetComponent<BubbleTweener>().MergeIntoOther(bubble.BubbleTransform().position);
    }

    public void ClearNode()
    {
        BubbleManager.Instance.ClearNode(_currentNode.X, _currentNode.Y);
    }

    [ContextMenu("Pop")]
    public void Pop()
    {
        List<Vector2Int> neighbours = _currentNode.GetNeighbours();
        foreach (var bubble in neighbours)
        {
            if (BubbleManager.Instance.GetBubble(bubble.x, bubble.y) != null)
            {
                BubbleManager.Instance.GetBubble(bubble.x, bubble.y).BubbleTransform().GetComponent<BubbleTweener>().PopTween();
                BubbleManager.Instance.GetBubble(bubble.x, bubble.y).ClearNode();
            }
        }
        GetComponent<BubbleTweener>().PopTween();
        ClearNode();
    }

    [ContextMenu("Neighbours")]
    public List<IBubble> CheckNeighbours()
    {
        return ReturnListOfNeighbours(_bubbleData.BubbleNumber);
    }

    public List<IBubble> CheckNeighboursOfNumber(int number)
    {
        return ReturnListOfNeighbours(number);
    }

    private List<IBubble> ReturnListOfNeighbours(int number)
    {
        List<Vector2Int> neighbours = _currentNode.GetNeighbours();

        List<IBubble> returnList = new List<IBubble>();
        foreach (var neighbour in neighbours)
        {
            if (neighbour != null)
            {
                var current = BubbleManager.Instance.GetBubble(neighbour.x, neighbour.y);
                if (current != null)
                {
                    if (current.BubbleNumber() == number)
                    {
                        returnList.Add(current);
                    }
                }
            }
        }

        return returnList;
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

    public int BubbleNumber()
    {
        return _bubbleData.BubbleNumber;
    }

    public Transform BubbleTransform()
    {
        return transform;
    }

    public Vector2Int BubbleCoordinate()
    {
        return new Vector2Int(_currentNode.X, _currentNode.Y);
    }
}
