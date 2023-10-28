using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSP_Node
{
    public BSP_Node leftTree;
    public BSP_Node rightTree;
    public BSP_Node parentTree;
    public RectInt treeSize;

    public BSP_Node(int x, int y, int width, int height)
    {
        treeSize.x = x;
        treeSize.y = y;
        treeSize.width = width;
        treeSize.height = height;
    }
}
