using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HexNode
{
   private readonly int _x;
   private readonly int _y;
   private readonly int _z;

   public HexNode(int x, int y)
   {
      _x = x;
      _y = y;
      _z = -(_x + _y);
   }
}
