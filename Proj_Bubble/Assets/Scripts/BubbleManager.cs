using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleManager : MonoBehaviour
{
    public static BubbleManager Instance;

    private HexGridManager _gridManager;

    private BubbleSO[] _bubbleSOs;

    private Dictionary<HexNode, IBubble> _bubbles = new Dictionary<HexNode, IBubble>();
    public Dictionary<int, BubbleSO> bubbleDataDictionary = new Dictionary<int, BubbleSO>();

    public GameObject bubblePrefab;

    private int heightCounter = HexGridManager.height - 1;

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

        _bubbleSOs = Resources.LoadAll<BubbleSO>("SoAssets/RegularBubbles");
        //if (_bubbleSOs != null) Debug.Log(_bubbleSOs.Length);
    }

    private void Start()
    {
        InitializeDictionary();
        CreateBubbleRow();
        CreateBubbleRow();
        CreateBubbleRow();
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

        foreach (var bData in _bubbleSOs)
        {
            if (!bubbleDataDictionary.ContainsKey(bData.BubbleNumber))
            {
                bubbleDataDictionary.Add(bData.BubbleNumber, bData);
            }
        }
    }

    public IBubble GetBubble(int x, int y)
    {
        return _bubbles[_gridManager.Nodes[x, y]];
    }

    public void ClearNode(int x, int y)
    {
        _bubbles[_gridManager.Nodes[x, y]] = null;
    }

    public Vector3 WorldNodePos(int x, int y)
    {
        return _gridManager.worldNodes[x, y].transform.position;
    }

    public void CreateBubble(int x , int y, int data)
    {
        var worldNode = _gridManager.worldNodes[x, y];
        Bubble go = Instantiate(bubblePrefab, new Vector3(worldNode.transform.position.x, worldNode.transform.position.y, 0), Quaternion.identity).GetComponent<Bubble>();
        Debug.Log("Data requested for number  " + data);
        go.Init(GetBubbleData(data));
        _bubbles[_gridManager.Nodes[x, y]] = go;
        go.CurrentNode = _gridManager.Nodes[x, y];
    }

    [ContextMenu("CreateRow")]
    public void CreateBubbleRow()
    {
        Debug.Log("Here");
        for (int i = HexGridManager.width - 1; i > -1; i--)
        {
            var worldNodePosition = _gridManager.worldNodes[i, heightCounter].transform.position;
            Bubble go = Instantiate(bubblePrefab, new Vector3(worldNodePosition.x, worldNodePosition.y, 0), Quaternion.identity).GetComponent<Bubble>();
            go.Init(_bubbleSOs[Random.Range(0, _bubbleSOs.Length)]);
            _bubbles[_gridManager.Nodes[i, heightCounter]] = go;
            go.CurrentNode = _gridManager.Nodes[i, heightCounter];
            go.CheckNeighbours();
        }

        heightCounter--;
    }

    public BubbleSO GetBubbleData(int bubbleData)
    {
        return bubbleDataDictionary[bubbleData];
    }

    public void PushAllDown()
    {
        
    }

    public void PushAllUp()
    {
        
    }
}
