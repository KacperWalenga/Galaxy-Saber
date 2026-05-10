using System;
using UnityEngine;

[Serializable]
public class BeatData
{
   public float Time;
   public int LineIndex;
   public int LineLayer;

   public BeatData(float time, int lineIndex, int lineLayer)
   {
      Time = time;
      LineIndex = lineIndex; 
      LineLayer = lineLayer;
   }
}
