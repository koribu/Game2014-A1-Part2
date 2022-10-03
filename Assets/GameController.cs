using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Instruction()
    {
        SceneManager.LoadScene(1);
    }
    public void Gameplay()
    {
        SceneManager.LoadScene(2);
    }
    public void GameOver()
    {
        SceneManager.LoadScene(3);
    }
}
