using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    private TMP_Text label;
    Vector2Int coordinate = new Vector2Int();

    private WayPoint wayPoint;

    private void Awake()
    {
        label = GetComponent<TMP_Text>();
        label.enabled = false;

        wayPoint = GetComponentInParent<WayPoint>();
        DisplayCoordinate();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinate();
            DisplayObjectName();
        }

        ColorCoordinates();
        ToggleLabels();
    }

    private void DisplayCoordinate()
    {
        coordinate.x = Mathf.RoundToInt(transform.parent.transform.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinate.y = Mathf.RoundToInt(transform.parent.transform.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = $"{coordinate.x}:{coordinate.y}";
    }

    private void DisplayObjectName()
    {
        transform.parent.name =coordinate.ToString();
    }

    private void ColorCoordinates()
    {
        if (wayPoint.IsPlaceable)
        {
            label.color = defaultColor;
        } else
        {
            label.color = blockedColor;
        }
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;
        }
    }
}
