using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HexNode
{
   private readonly int _x;
   private readonly int _y;

   public int X => _x;
   public int Y => _y;
   private List<Vector2Int> _neighbourList;

   public HexNode(int x, int y)
   {
      _x = x;
      _y = y;
      _neighbourList = new List<Vector2Int>();
      GenerateNeighbours();
   }

   private void GenerateNeighbours()
   {
      if (_y < HexGridManager.height - 1)
      {
         _neighbourList.Add(new Vector2Int(_x, _y + 1));
      }
      if (_y != 0)
      {
         _neighbourList.Add(new Vector2Int(_x, _y - 1));
      }
      if (_x != 0)
      {
         _neighbourList.Add(new Vector2Int(_x - 1, _y));
      }
      if (_x < HexGridManager.width - 1)
      {
         _neighbourList.Add(new Vector2Int(_x + 1, _y));
      }
      if (_y < HexGridManager.height - 1 && _x < HexGridManager.width - 1)
      {
         _neighbourList.Add(new Vector2Int(_x + 1, _y + 1));
      }
      if (_y != 0 && _x != 0)
      {
         _neighbourList.Add(new Vector2Int(_x - 1, _y - 1));
      }
   }

   public List<Vector2Int> GetNeighbours()
   {
      return _neighbourList;
   }
}
