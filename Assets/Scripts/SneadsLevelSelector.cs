using UnityEngine;
using UnityEngine.SceneManagement;

public class SneadsLevelSelector : MonoBehaviour
{
    string debugText;
    
    void Update()
    {
        LevelController();
    }

    void Start()
    {
        debugText = "";
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 100, 1000, 40), debugText);
    }

    private void LevelController()
    {
        int buildArraySize = SceneManager.sceneCountInBuildSettings;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        //Loads next scene with '=' key
        if (Input.GetKeyDown(KeyCode.Equals) && currentIndex < buildArraySize - 1)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        //Loads previous scene with '-' key
        if (Input.GetKeyDown(KeyCode.Minus) && currentIndex != 0)
        {
            SceneManager.LoadScene(currentIndex - 1);
        }
        //Loads the current scene again with 'backspace' key
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(currentIndex);
        }
        //Hold 'backslash' to display debug text
        if (Input.GetKey(KeyCode.Backslash))
        {
            debugText = "Number of Levels: " + buildArraySize + "\n" + "Current Level: " + (currentIndex + 1);
        }
        if (Input.GetKeyUp(KeyCode.Backslash))
        {
            debugText = "";
        }
    }
}
