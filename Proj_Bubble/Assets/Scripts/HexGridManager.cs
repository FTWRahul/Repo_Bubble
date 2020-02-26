using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    [SerializeField]
    public static int height = 9;
    [SerializeField]
    public static int width = 6;

    [SerializeField] private float radialOffest = .5f;

    public HexNode[,] Nodes;
    public WorldNode[,] worldNodes;
    
    private void Awake()
    {
        CreateGrid();
    }

    public void CreateGrid()
    {
        Nodes = new HexNode[width, height];
        worldNodes = new WorldNode[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool offset = false;
                GameObject go = new GameObject("Node : "+x + ", " + y); //GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.transform.parent = this.transform;
                if (y % 2 == 1)
                {
                    go.transform.position = new Vector3(x + radialOffest,y,0);
                    offset = true;
                }
                else
                {
                    go.transform.position = new Vector3(x,y,0);
                }

                worldNodes[x, y] = go.AddComponent<WorldNode>();
                HexNode newNode = new HexNode(x,y, offset);
                Nodes[x, y] = newNode;
                worldNodes[x, y].hexNode = newNode;
                //Debug.Log("node " +x+ " ," +y+ " has this many neighbours --> " +newNode.GetNeighbours().Count);
            }
        }
    }
}
