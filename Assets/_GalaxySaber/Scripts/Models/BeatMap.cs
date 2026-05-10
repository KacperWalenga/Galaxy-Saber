using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BeatMap
{
    public List<BeatData> BeatData;

    public BeatMap(List<BeatData> beatData)
    {
        BeatData = beatData;
    }
}
