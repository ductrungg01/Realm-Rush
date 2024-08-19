using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform parentInHierachy;
    [SerializeField] [Min(0)] private float delayTime = 1;

    [SerializeField][Min(0)] private int poolSize = 10;

    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; ++i)
        {
            pool[i] = Instantiate(enemyPrefab, parentInHierachy);
            pool[i].SetActive(false);
        }
    }

    private void OnEnablePool()
    {
        foreach (var pool in pool)
        {
            if (!pool.activeInHierarchy)
            {
                pool.SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            OnEnablePool();
            yield return new WaitForSeconds(delayTime);
        }
    }
}
