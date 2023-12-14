using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject previousButton;
    public GameObject nextButton;
    public GameObject startGameButton;
    public GameObject mainMenuPanel;
    public GameObject levelSelectionPanel;
    public GameObject[] pages; // An array of your instruction page panels
    public int currentPage = 0;

    public void StartLevelSelection()
    {
        mainMenuPanel.SetActive(false); // Hide the main menu
        levelSelectionPanel.SetActive(true); // Show the level selection
    }
    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            pages[currentPage].SetActive(false);
            currentPage++;
            pages[currentPage].SetActive(true);

            // If the current page is the last one (page 3 in a zero-indexed array),
            // activate the 'Start Game' button.
            if (currentPage == pages.Length - 1)
            {
                startGameButton.SetActive(true);
            }
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            // oing back from the last page, deactivate the 'Start Game' button.
            if (currentPage == pages.Length - 1)
            {
                startGameButton.SetActive(false);
            }

            pages[currentPage].SetActive(false);
            currentPage--;
            pages[currentPage].SetActive(true);
        }
    }

    public void ShowInstructions()
    {
        // Activate the first page and deactivate the main menu
        mainMenuPanel.SetActive(false);
        pages[0].SetActive(true);
        currentPage = 0;
        mainMenuPanel.SetActive(true);
        previousButton.SetActive(true); // Previous button should be hidden on the first page
        nextButton.SetActive(true);
    }

    public void HideInstructions()
    {
        // Deactivate all instruction pages and reactivate the main menu
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        mainMenuPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        previousButton.SetActive(false); // Ensure the Previous button is hidden
        nextButton.SetActive(false);
    }
    // Update is called once per frame
    public void QuitGame()
    {
        // This will close the application. Note that this will only work in a built game, not in the Unity editor.
        Application.Quit();
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("Menu Start"); 
        Time.timeScale = 1;
    }

    // Call these methods when the Easy, Normal, Hard buttons are clicked
    public void LoadEasyLevel()
    {
        SceneManager.LoadScene("Restaurant view1"); 
    }

    public void LoadNormalLevel()
    {
        SceneManager.LoadScene("Restaurant view2"); 
    }

    public void LoadHardLevel()
    {
        SceneManager.LoadScene("Restaurant view3"); 
    }
}
