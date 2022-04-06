using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public bool walkable;
    public Vector2 worldPosition;
    public int gridX, gridY;

    public int gCost; // distance from startnode, or start target
    public int hCost; // distance from endnode, or end target
    public Node parent;
    int heapIndex;

    public Node(bool walkable, Vector2 worldPos, int x, int y)
    {
        worldPosition = worldPos;
        this.walkable = walkable;
        gridX = x;
        gridY = y;
    }
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }
    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}