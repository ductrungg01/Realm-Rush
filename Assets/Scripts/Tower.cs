using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField][Min(0)] private int cost = 75; 

    public bool CreateTower(Tower towerPrefab, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank != null && bank.CurrentAmount >= cost)
        {
            bank.WithDraw(cost);

            Instantiate(towerPrefab.gameObject, position, Quaternion.identity);
            return true;
        }

        return false;
    }
}
