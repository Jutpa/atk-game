using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript: MonoBehaviour
{
    public GameObject PlayerStats;
    public GameObject Menu;
    public GameObject HelpMenu;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject DeathScreen;
    public GameObject IntroScreen;
    public ScoreScript scoreScript;
    public RotateScript rotateScript;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.SetActive(false);
        Menu.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        DeathScreen.SetActive(false);
        IntroScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }

        if (scoreScript.trueScore == 10000 || rotateScript.rotXval >= 360)
        {
            Win();
        }
    }

    // Function for "Return to Menu" button, loads the main menu
    public void MenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    // Function for "Help" button, changes the menu to show controls and how to play
    public void HelpButton()
    {
        PlayerStats.SetActive(false);
        Menu.SetActive(false);
        HelpMenu.SetActive(true);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        DeathScreen.SetActive(false);
        IntroScreen.SetActive(false);
        Time.timeScale = 0.0f;
    }

    // Function for "Back" button on the controls menu and pressing escape, changes the menu back to the main menu
    public void BackButton()
    {
        PlayerStats.SetActive(false);
        Menu.SetActive(true);
        HelpMenu.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        DeathScreen.SetActive(false);
        IntroScreen.SetActive(false);
        Time.timeScale = 0.0f;
    }

    // Function for "Back to Game" button and for starting/restarting level, changes the menu back to the regular game and increases time back to normal flow
    public void GameButton()
    {
        PlayerStats.SetActive(true);
        Menu.SetActive(false);
        HelpMenu.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        DeathScreen.SetActive(false);
        IntroScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }

    // Function for winning the game by killing 100 zombies/gaining 10,000 points.
    public void Win()
    {
        PlayerStats.SetActive(false);
        Menu.SetActive(false);
        HelpMenu.SetActive(false);
        WinScreen.SetActive(true);
        LoseScreen.SetActive(false);
        DeathScreen.SetActive(false);
        IntroScreen.SetActive(false);
        Time.timeScale = 0.0f;
    }

    // Function for losing the game by letting the zombies eat the chickens.
    public void Lose()
    {
        PlayerStats.SetActive(false);
        Menu.SetActive(false);
        HelpMenu.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(true);
        DeathScreen.SetActive(false);
        IntroScreen.SetActive(false);
        Time.timeScale = 0.0f;
    }

    // Function for losing the game by getting eaten by the zombies.
    public void GameOver()
    {
        PlayerStats.SetActive(false);
        Menu.SetActive(false);
        HelpMenu.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        DeathScreen.SetActive(true);
        IntroScreen.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
