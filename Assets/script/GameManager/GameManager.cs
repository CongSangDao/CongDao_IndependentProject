using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int unsatisfiedCustomers = 0;
    private int maxUnsatisfiedCustomers = 2;
    public int satisfiedCustomers = 0;
    public int requiredSatisfactionsForWin = 5;
    public TextMeshProUGUI gameOverText;

    // Adding a queue to manage customer wait times
    public Queue<CustomerMovement> customerQueue = new Queue<CustomerMovement>();

    // Singleton pattern to make it easy to reference this script from others
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void CustomerSatisfied(CustomerMovement customer)
    {
        Debug.Log("Satisfied a customer. Unsatisfied count before: " + unsatisfiedCustomers);
        satisfiedCustomers++;
        if (satisfiedCustomers >= requiredSatisfactionsForWin)
        {
            GameWon();
        }
        if (unsatisfiedCustomers > 0)
            unsatisfiedCustomers--;

        // Removing the customer from the queue once they are satisfied
        
    }

    public void CustomerBecameUnsatisfied(CustomerMovement customer)
    {
        unsatisfiedCustomers++;
        Debug.Log("Unsatisfied customer count: " + unsatisfiedCustomers);

        // Removing the unsatisfied customer from the queue
        

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

    private void GameWon()
    {
        Debug.Log("GameWon!");

        // You can display a win message similar to the game over text
        // Assuming you have a TextMeshProUGUI winText similar to gameOverText
        

        // Stop all movement or end the level as per your game design
        // Time.timeScale = 0; // Uncomment if you want to stop the game
    }
}
