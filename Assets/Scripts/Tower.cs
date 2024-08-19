using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] [Min(0)] private int cost = 75;
    [SerializeField] [Min(0)] private float delayBuild = 2f;

    private void Start()
    {
        StartCoroutine(Build());
    }

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

    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayBuild);
        }
    }
}
