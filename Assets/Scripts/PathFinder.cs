using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    [SerializeField] private Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }


    Vector2Int[] directions = {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid;

    Node currentSearchNode;
    Node startNode;
    Node destinationNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node> ();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        grid = gridManager.Grid;
        startNode = grid[startCoordinates];
        destinationNode = grid[destinationCoordinates];
    }

    private void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);    
    }

    public List<Node> GetNewPath(Vector2Int coord)
    {
        gridManager.ResetNodes();
        BFS(coord);
        return BuildPath();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();  

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoord = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighborCoord))
            {
                neighbors.Add(grid[neighborCoord]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor); 
            }
        }
    }

    void BFS(Vector2Int coord)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coord]);
        reached.Add(coord, grid[coord]);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;

            ExploreNeighbors();

            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currNode = destinationNode;

        path.Add(currNode); 
        currNode.isPath = true;

        while (currNode.connectedTo != null)
        {
            currNode = currNode.connectedTo; 
            path.Add(currNode);
            currNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int coordinate)
    {
        if (grid.ContainsKey(coordinate))
        {
            bool prevState = grid[coordinate].isWalkable;

            grid[coordinate].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinate].isWalkable = prevState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    public void NotifyReceiver()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
