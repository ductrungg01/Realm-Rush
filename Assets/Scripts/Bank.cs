using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField][Min(0)] private int startBalance = 150;
    private int currentBalance = 0;
    public int CurrentAmount {  get { return currentBalance; } }

    [SerializeField] private TMP_Text balanceText;

    private void Awake()
    {
        currentBalance = startBalance;
        UpdateBalanceText();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);

        UpdateBalanceText();
    }

    public void WithDraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);

        if (currentBalance < 0)
        {
            // Lose
            ReloadScene();
        }

        UpdateBalanceText();
    }

    private void UpdateBalanceText()
    {
        balanceText.text = $"GOLD: {currentBalance}";
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
