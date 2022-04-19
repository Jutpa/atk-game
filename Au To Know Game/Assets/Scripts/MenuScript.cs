using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject HelpMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenuButton();
        }
    }
    
    // Function for "Play" button, loads the level
    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
    
    // Function for "How to Play" button, changes the menu to show controls and how to play
    public void HelpButton()
    {
        MainMenu.SetActive(false);
        HelpMenu.SetActive(true);
    }

    // Function for "Back" button on the controls menu and for Start function, changes the menu back to the main menu
    public void MainMenuButton()
    {
        MainMenu.SetActive(true);
        HelpMenu.SetActive(false);
    }

    // Function for "Quit" button, closes the application
    public void QuitButton()
    {
        Application.Quit();
    }
}
