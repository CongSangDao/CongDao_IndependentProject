using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int unsatisfiedCustomers = 0;
    private int maxUnsatisfiedCustomers = 2;
    public int satisfiedCustomers = 0;
    public int requiredSatisfactionsForWin = 5;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI satisfactionText;
    public TextMeshProUGUI unsatisfactionText;
    //UI
    public DebugUIManager debugUIManager;
    public TextMeshProUGUI ordersText;
    public TextMeshProUGUI chefStatusText;
    // Adding a queue to manage customer wait times
    public Queue<CustomerMovement> customerQueue = new Queue<CustomerMovement>();

    // Singleton pattern to make it easy to reference this script from others
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


    }
    private void OnLevelWasLoaded(int level)
    {
        ResetGameState();
    }

    public void CustomerSatisfied(CustomerMovement customer)
    {
        satisfiedCustomers++;
        UpdateSatisfactionUI();
        if (satisfiedCustomers >= requiredSatisfactionsForWin)
        {
            GameWon();
        }
        if (unsatisfiedCustomers > 0)
            unsatisfiedCustomers--;

        // Removing the customer from the queue once they are satisfied
        StartCoroutine(ClearDebugMessagesAfterDelay(5f));
    }

    public void CustomerBecameUnsatisfied(CustomerMovement customer)
    {
        unsatisfiedCustomers++;
        UpdateSatisfactionUI();
        // Removing the unsatisfied customer from the queue
        StartCoroutine(ClearDebugMessagesAfterDelay(5f));

        // Check for game over condition
        if (unsatisfiedCustomers >= maxUnsatisfiedCustomers)
        {
            GameOver();
        }
    }

    public void UpdateSatisfactionUI()
    {
        satisfactionText.text = "Satisfaction: " + satisfiedCustomers;
        unsatisfactionText.text = "Unsatisfaction: " + unsatisfiedCustomers;

    }

    public void GameOver()
    {
        Debug.Log("GameOver!");
        // Active the button
       
        SceneManager.LoadScene("Play Again");

        // Stop all movement
        Time.timeScale = 1;
    }

     void GameWon()
    {
        Debug.Log("GameWon!");
        SceneManager.LoadScene("EndGame");
    }
   
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
        Time.timeScale = 1;
    }

    private IEnumerator ClearDebugMessagesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    public void ResetGameState()
    {
        // Reset all relevant game state variables
        satisfiedCustomers = 0;
        unsatisfiedCustomers = 0;
        if (satisfactionText != null && unsatisfactionText != null)
        {
            UpdateSatisfactionUI();
        }
       
    }
    public void StartNewGame()
    {
        // Reset game state before loading the new scene
        ResetGameState();

        // Load the game scene
        SceneManager.LoadScene("Restaurant view");
    }

    public void PlayAgain()
    {
        Debug.Log("Play Again button clicked. Loading RestaurantView...");
        StartNewGame();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find and set the references to your UI elements here

        // Add any other UI elements you need to find after scene load

        ResetGameState();
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
