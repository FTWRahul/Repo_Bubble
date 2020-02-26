using UnityEngine;

public interface IBubble
{
    //TODO : Fix HUGE bad interface
    void Init(BubbleSO bubbleData);
    void Merge();
    void Pop();
    void GetNeighbour();
    Vector2Int GetNearestAvailableNeighbour(Vector2 from);
}