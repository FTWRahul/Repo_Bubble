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

    public IBubble GetBubble(int x, int y)
    {
        return _bubbles[_gridManager.Nodes[x, y]];
    }

    public Vector3 WorldNodePos(int x, int y)
    {
        return _gridManager.worldNodes[x, y].transform.position;
    }

    [ContextMenu("Create entire grid")]
    public void CreateBubble()
    {
        foreach (var worldNode in _gridManager.worldNodes)
        {
            Bubble go = Instantiate(bubblePrefab, new Vector3(worldNode.transform.position.x, worldNode.transform.position.y, 0), Quaternion.identity).GetComponent<Bubble>();
            go.Init(_bubbleSOs[Random.Range(0, _bubbleSOs.Length)]);
            _bubbles[_gridManager.Nodes[worldNode.hexNode.X, worldNode.hexNode.Y]] = go;
            go.CurrentNode = _gridManager.Nodes[worldNode.hexNode.X, worldNode.hexNode.Y];
            go.GetNeighbour();
        }
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
            go.GetNeighbour();
        }

        heightCounter--;
    }

    public void PushAllDown()
    {
        
    }

    public void PushAllUp()
    {
        
    }
}
