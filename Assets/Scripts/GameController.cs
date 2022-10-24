/*--------------------Space Madness----------------------------------
 * -----------a space shooter game with chaotic atmosphere-----------
 * Name: Sinan Kolip
 * Student Number: 101312965
 * Last Modified Time: 10/02/2022
 * Scenes added, UI and some dummy sprites added to scenes, assets added to the project
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 class GameController : MonoBehaviour
{
    [SerializeField]
    AudioClip _lobbyMusic;
    AudioSource _MusicSource;

    // Start is called before the first frame update
    void Start()
    {
        _MusicSource = GetComponent<AudioSource>();
        _MusicSource.clip = _lobbyMusic; 
        _MusicSource.Play();
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
