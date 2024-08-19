using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }


    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            isPlaceable = !towerPrefab.CreateTower(towerPrefab, transform.position);
        }
    }
}
