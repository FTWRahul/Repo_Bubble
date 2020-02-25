using UnityEngine;

public interface IBubble
{
    void Init(BubbleSO bubbleData);
    void Merge();
    void Pop();
    void GetNeighbour();
}