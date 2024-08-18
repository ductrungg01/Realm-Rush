using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    private TMP_Text label;
    Vector2Int coordinate = new Vector2Int();

    private void Awake()
    {
        label = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinate();
            DisplayObjectName();
        }       
    }

    private void DisplayCoordinate()
    {
        coordinate.x = Mathf.RoundToInt(transform.parent.transform.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinate.y = Mathf.RoundToInt(transform.parent.transform.position.y / UnityEditor.EditorSnapSettings.move.y);

        label.text = $"{coordinate.x}:{coordinate.y}";
    }

    private void DisplayObjectName()
    {
        transform.parent.name =coordinate.ToString();
    }
}
