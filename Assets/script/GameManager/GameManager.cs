using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int unsatisfiedCustomers = 0; // Tracks number of unsatisfied customers
    public int maxUnsatisfiedCustomers = 2; // Max unsatisfied customers before game over

    // Singleton pattern to make it easy to reference this script from others
    public static GameManager instance;
    public TextMeshProUGUI gameOverText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void CustomerSatisfied()
    {
        Debug.Log("Satisfied a customer. Unsatisfied count before: " + unsatisfiedCustomers);
        if (unsatisfiedCustomers > 0)
            unsatisfiedCustomers--;
    }

    public void CustomerBecameUnsatisfied()
    {
        unsatisfiedCustomers++;
        Debug.Log("Unsatisfied customer count before: " + unsatisfiedCustomers);


        // Check for game over condition
        if (unsatisfiedCustomers >= maxUnsatisfiedCustomers)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("GameOver!");

        // Show the game over text
        gameOverText.gameObject.SetActive(true);

        // Stop all movement
        Time.timeScale = 0;
    }
}
