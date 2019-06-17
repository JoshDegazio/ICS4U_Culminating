/* Josh Degazio
 * Started On: May 28th, 2019
 * An endless, topdown zombie game at it's core.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonController : MonoBehaviour
{
    //When the method is called
    public void StartLevel()
    {
        //Load scene "Level"
        SceneManager.LoadSceneAsync("Level");
    }

    //When the method is called
    public void OpenSettings()
    {
        //Load scene "Options"
    }

    //When the method is called
    public void MainMenu()
    {
        //Load scene "Mainmenu"
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void QuitGame()
    {
        //Close the program
        Application.Quit();
    }
}
