using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeHandeler
{
    private List<IBubble> _checkedBubbles = new List<IBubble>();
    private List<IBubble> _currentNeighbours = new List<IBubble>();
    private List<IBubble> _unCheckedBubbles = new List<IBubble>();
    private IBubble _invoker;
    private int _mergeResult = 0;

    public MergeHandeler(IBubble invoker)
    {
        _invoker = invoker;
    }

    public void StartCheck()
    {
        _currentNeighbours = _invoker.CheckNeighbours();
        if (_currentNeighbours.Count > 0 )
        {
            _checkedBubbles.Add(_invoker);
            _unCheckedBubbles = _currentNeighbours;
            CheckNeighbourBubble();
        }
    }

    private void CheckNeighbourBubble()
    {
        for (int i = 0; i < _unCheckedBubbles.Count; i++)
        {
            _currentNeighbours = _unCheckedBubbles[i].CheckNeighbours();
            List<IBubble> newUnchecked = new List<IBubble>();
            if (_currentNeighbours != null)
            {
                foreach (var current in _currentNeighbours)
                {
                    if (!_checkedBubbles.Contains(current) && !_unCheckedBubbles.Contains(current))
                    {
                        newUnchecked.Add(current);
                    }
                }
            }

            for (int j = 0; j < newUnchecked.Count; j++)
            {
                _unCheckedBubbles.Add(newUnchecked[j]);
            }
            _checkedBubbles.Add(_unCheckedBubbles[i]);
        }

        List<IBubble> tempUnchecked = _unCheckedBubbles;
        foreach (var unChecked in tempUnchecked)
        {
            if (_checkedBubbles.Contains(unChecked))
            {
                _unCheckedBubbles.Remove(unChecked);
            }
        }

        if (_unCheckedBubbles.Count > 0)
        {
            CheckNeighbourBubble();
        }
        else
        {
            FindResult();
        }
    }

    private void FindResult()
    {
        _mergeResult = _invoker.BubbleNumber();
        for (int i = 0; i < _checkedBubbles.Count - 1; i++)
        {
            _mergeResult += _mergeResult;
        }
    }
}
