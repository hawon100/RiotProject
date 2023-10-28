using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileData
{
    [Serializable]
    public struct rowData{
        public int[] row;
    }

    public rowData[] rows = new rowData[10];
}
