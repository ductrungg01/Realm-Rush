using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    [Tooltip("World grid size - should match with UnityEditor snap settings")]
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize { get {  return unityGridSize; } } 

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid {get { return grid; } }
    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coord)
    {
        if (grid.ContainsKey(coord))
            return grid[coord];
        return null;    
    }

    public void BlockNode(Vector2Int coord)
    {
        if (grid.ContainsKey(coord))
        {
            grid[coord].isWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach (var entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored  = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);
        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coord)
    {
        Vector3 pos = new Vector3();
        pos.x = unityGridSize * coord.x;
        pos.z = unityGridSize * coord.y;

        return pos;
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; ++x)
        {
            for (int y = 0; y < gridSize.y; ++y)
            {
                Vector2Int coord = new Vector2Int(x, y);

                grid.Add(coord, new Node(coord, true));
            }
        }

    }
}
