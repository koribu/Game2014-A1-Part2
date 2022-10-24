using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    Boundary spawnZone;

    [Range(1, 4)]
    public int enemyNumber = 0;

    public bool _isGameScene = false;

    [SerializeField]
    private GameObject enemyPrefab, asteroidPrefab;

    public int enemyGroupNum, asteroidGroupNum, currentSequence;

    [SerializeField]
    List<SpawnSequence> spawnSequenceList;

    // Start is called before the first frame update
    void Start()
    {
        currentSequence = 0;
        nextSpawnGroup(spawnSequenceList[currentSequence]);
    }

    private void Update()
    {
        if (enemyGroupNum == 0 && asteroidGroupNum == 0)
        {
            if(spawnSequenceList.Count > currentSequence)
                 nextSpawnGroup(spawnSequenceList[++currentSequence]);
        }
            
    }

    // Update is called once per frame
    public void nextSpawnGroup(SpawnSequence spawnList)
    {
        enemyGroupNum = spawnList.enemy; asteroidGroupNum = spawnList.asteroid;

        StartCoroutine(SpawnRoutine());
    }
    public void enemyDestroyed()
    {
        enemyGroupNum--;
    }
    public void asteroidDestroyed()
    {
        asteroidGroupNum--;
    }
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3);
        int e = enemyGroupNum;
        int a = asteroidGroupNum;
        for (int i = 0; i < e; i++)
        {
            var enemy = Instantiate(enemyPrefab);       
            yield return new WaitForSeconds(Random.Range(.5f, 1));
        }
        for (int i = 0; i < a; i++)
        {
            var enemy = Instantiate(asteroidPrefab);
            yield return new WaitForSeconds(Random.Range(.5f, 1));
        }

  

    }

}
