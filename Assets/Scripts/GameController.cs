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
    AudioClip _lobbyMusic, _buttonSound;
    AudioSource _MusicSource;
    float buttonDelayTime = 1;

    [Range(1, 4)]
    public int enemyNumber = 0;

    public bool _isGameScene = false;

    private List<GameObject> enemyList;
    [SerializeField]
    private GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if(_isGameScene)
        BuildEnemyList();

        _MusicSource = GetComponent<AudioSource>();
        _MusicSource.clip = _lobbyMusic; 
        _MusicSource.Play();
    }

    void BuildEnemyList()
    {
        enemyList = new List<GameObject>();

        for (int i = 0; i < enemyNumber; i++)
        {
            var enemy = Instantiate(enemyPrefab);
            enemyList.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MainMenu()
    {
        StartCoroutine(DelayedLoadSceneRoutine(buttonDelayTime, 0));
            }
    public void Instruction()
    {
        StartCoroutine(DelayedLoadSceneRoutine(buttonDelayTime, 1));
    }
    public void Gameplay()
    {
        StartCoroutine(DelayedLoadSceneRoutine(buttonDelayTime, 2));
    }
    public void GameOver()
    {
        StartCoroutine(DelayedLoadSceneRoutine(buttonDelayTime, 3));
    }

    IEnumerator DelayedLoadSceneRoutine(float waitTime, int SceneNum)
    {
        _MusicSource.clip = _buttonSound;
        _MusicSource.Play();
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneNum);
    }
}
