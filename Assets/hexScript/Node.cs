using UnityEngine;
using System.Collections;


public class Node  {
	
	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;
    public Color nodeColor;
	public Node parent;
    public bool flat;
	
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY,Color color) {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
        nodeColor = color;
        flat = false;
	}

    public void setWalkable(bool Walkable) {
        walkable = Walkable;
    }

    public void setNodeColor(Color color) {
        nodeColor = color;
    }

    public void setNodeFlat(bool Flat)
    {
        flat = Flat;
    }
}