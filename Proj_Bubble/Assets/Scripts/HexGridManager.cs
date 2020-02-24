using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    [SerializeField]
    private int height = 9;
    [SerializeField]
    private int width = 6;

    [SerializeField] private float radialOffest = .5f;

    public HexNode[,] Nodes;
    
    

    private void Start()
    {
        CreateGrid();
    }

    public void CreateGrid()
    {
        Nodes = new HexNode[width,height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                HexNode newNode = new HexNode(x,y);
                Nodes[x, y] = newNode;
                GameObject go = new GameObject("Node : "+x + " ," + y); //GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.transform.parent = this.transform;
                if (y % 2 == 1)
                {
                    go.transform.position = new Vector3(x + radialOffest,y,0);
                    Debug.Log("Here");
                }
                else
                {
                    go.transform.position = new Vector3(x,y,0);
                }
                
            }
        }
    }
}
