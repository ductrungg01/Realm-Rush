using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField][Min(0)] private int startBalance = 150;
    private int currentBalance = 0;

    public int CurrentAmount {  get { return currentBalance; } }

    private void Awake()
    {
        currentBalance = startBalance;
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
    }

    public void WithDraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);

        if (currentBalance < 0)
        {
            // Lose
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentBalance);
    }
}
