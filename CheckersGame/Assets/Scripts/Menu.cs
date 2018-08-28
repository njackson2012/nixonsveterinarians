using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu Instance { set; get; }   

    //public InputField nameInput;
    public GameObject mainMenuButtons;
    public GameObject newGamePanel;
    public GameObject joinGamePanel;

    // runs when application starts
    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        newGamePanel.SetActive(false);
        joinGamePanel.SetActive(false);
    }

    //clicked the join game button
    public void JoinGameButton()
    {
        mainMenuButtons.SetActive(false);
        joinGamePanel.SetActive(true);
    }

    //clicked the join game confirm button
    public void JoinGameConfirmButton()
    {
        SceneManager.LoadScene("Game");
    }

    //clicked the join gam's back button
    public void JoinGameCancelButton()
    {
        mainMenuButtons.SetActive(true);
        joinGamePanel.SetActive(false);
    }

    //clicked the new game button
    public void NewGameButton()
    {
        mainMenuButtons.SetActive(false);
        newGamePanel.SetActive(true);
    }

    //clicked the new game confirm button
    public void NewGameConfirmButton()
    {
        SceneManager.LoadScene("Game");
    }

    //clicked the new game's back button
    public void NewGameCancelButton()
    {
        mainMenuButtons.SetActive(true);
        newGamePanel.SetActive(false);
    }

    //clicked the exit game button
    public void ExitGameButton()
    {
        Application.Quit();
    }
}
