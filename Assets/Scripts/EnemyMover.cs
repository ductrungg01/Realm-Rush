using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0, 5)] private float speed = 1f;

    private List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

    private void OnEnable()
    {
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void RecalculatePath()
    {
        path.Clear();

        path = pathFinder.GetNewPath(); 
    }

    private void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for (int i = 0; i < path.Count; ++i)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPos);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);

                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }
}
