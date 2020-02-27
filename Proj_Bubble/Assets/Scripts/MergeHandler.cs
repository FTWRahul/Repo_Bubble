using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class MergeHandler
{
    private List<IBubble> _checkedBubbles = new List<IBubble>();
    private List<IBubble> _currentNeighbours = new List<IBubble>();
    private List<IBubble> _unCheckedBubbles = new List<IBubble>();
    private IBubble _invoker;
    private int _mergeResult = 0;

    public MergeHandler(IBubble invoker)
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

        UpdateUncheckList(_unCheckedBubbles);
    }

    private void UpdateUncheckList(List<IBubble> toCheck)
    {
        List<IBubble> tempUnchecked = new List<IBubble>();
        foreach (var unChecked in _unCheckedBubbles)
        {
            tempUnchecked.Add(unChecked);
        }
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
        List<IBubble> tempUnchecked = new List<IBubble>();

        _mergeResult = _invoker.BubbleNumber();
        for (int i = 0; i < _checkedBubbles.Count - 1; i++)
        {
            _mergeResult += _mergeResult;
        }

        List<IBubble> finalList = new List<IBubble>();
        foreach (var bubble in _checkedBubbles)
        {
            finalList = bubble.CheckNeighboursOfNumber(_mergeResult);
            if (finalList != null)
            {
                MergeAndCreateNew(bubble);
                break;
            }
        }

        if (finalList.Count < 1)
        {
            MergeAndCreateNew(_invoker);
        }
    }

    private void MergeAndCreateNew(IBubble inBubble)
    {
        foreach (var bubble in _checkedBubbles)
        {
            bubble.Merge(inBubble);
        }

        BubbleManager.Instance.CreateBubble(inBubble.BubbleCoordinate().x, inBubble.BubbleCoordinate().y, _mergeResult);
        StartAfterDelay(inBubble);
    }

    private async void StartAfterDelay(IBubble inBubble)
    {
        await Task.Delay(400);
        BubbleManager.Instance.GetBubble(inBubble.BubbleCoordinate().x, inBubble.BubbleCoordinate().y).StartMerge();
        await Task.Delay(100);
        if (_mergeResult <= 2048)
        {
            BubbleManager.Instance.GetBubble(inBubble.BubbleCoordinate().x, inBubble.BubbleCoordinate().y).Pop();
        }
    }
}
