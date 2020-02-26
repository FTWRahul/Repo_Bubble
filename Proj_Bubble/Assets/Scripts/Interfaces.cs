using System.Collections.Generic;
using UnityEngine;

public interface IBubble
{
    //TODO : Fix HUGE bad interface
    void Init(BubbleSO bubbleData);
    void StartMerge();
    void Merge(IBubble bubble);
    void Pop();
    List<IBubble> CheckNeighbours();
    Vector2Int GetNearestAvailableNeighbour(Vector2 from);

    int BubbleNumber();
}