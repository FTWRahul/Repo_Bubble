using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public static BubbleManager Instance;

    private HexGridManager _gridManager;

    private BubbleSO[] _bubbleSOs;

    private Dictionary<HexNode, Bubble> _bubbles = new Dictionary<HexNode, Bubble>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        _gridManager = GetComponent<HexGridManager>();

        _bubbleSOs = Resources.LoadAll<BubbleSO>("SoAssets");
        if (_bubbleSOs != null) Debug.Log(_bubbleSOs.Length);
    }

    private void Start()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        foreach (var node in _gridManager.Nodes)
        {
            if (!_bubbles.ContainsKey(node))
            {
                _bubbles.Add(node, null);
            }
        }
    }

    public Bubble GetBubble(int x, int y)
    {
        return _bubbles[_gridManager.Nodes[x, y]];
    }
}
