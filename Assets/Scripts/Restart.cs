using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void restart()
    {
        SceneManager.LoadScene(1);
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void exit()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
