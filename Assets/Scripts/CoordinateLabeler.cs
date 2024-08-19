using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    Color defaultColor = Color.white;
    Color blockedColor = Color.gray;
    Color exploredColor = Color.yellow;
    Color pathColor = Color.red;

    private TMP_Text label;
    Vector2Int coordinate = new Vector2Int();

    GridManager gridManager;

    private void Awake()
    {
        label = GetComponent<TMP_Text>();
        gridManager = FindObjectOfType<GridManager>();

        label.enabled = !Application.isPlaying;

        DisplayCoordinate();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinate();
            DisplayObjectName();
        }

        SetLabelColor();
        ToggleLabels();
    }

    private void DisplayCoordinate()
    {
        UpdateCoordinates();

        label.text = $"{coordinate.x}:{coordinate.y}";
    }

    private void UpdateCoordinates()
    {
        if (gridManager == null) return;

        coordinate.x = Mathf.RoundToInt(transform.parent.transform.position.x / gridManager.UnityGridSize);
        coordinate.y = Mathf.RoundToInt(transform.parent.transform.position.z / gridManager.UnityGridSize);
    }

    private void DisplayObjectName()
    {
        transform.parent.name =coordinate.ToString();
    }

    private void SetLabelColor()
    {
        if (gridManager == null) return;

        Node node = gridManager.GetNode(coordinate);

        if (node == null) return;   

        if (!node.isWalkable) label.color = blockedColor;
        else if (node.isPath) label.color = pathColor;
        else if (node.isExplored) label.color = exploredColor;
        else label.color = defaultColor;
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;
        }
    }
}
