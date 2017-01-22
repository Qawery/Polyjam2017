using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas howToPlay;

    public void Start()
    {
        mainMenu.enabled = true;
        howToPlay.enabled = false;
    }

    public void StartButton()
    {
        Application.LoadLevel("level_01");
    }

    public void howToPlayButton()
    {
        mainMenu.enabled = false;
        howToPlay.enabled = true;
    }

    public void backButton()
    {
        mainMenu.enabled = true;
        howToPlay.enabled = false;
    }

    public void exitButton()
    {
        Application.Quit();
    }
}
