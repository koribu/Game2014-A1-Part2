/*--------------------Space Madness----------------------------------
 * -----------a space shooter game with chaotic atmosphere-----------
 * Name: Sinan Kolip
 * Student Number: 101312965
 * Last Modified Time: 10/24/2022
 * Bullet manager that prepare bullet pool and provide bullets for player and enemy
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    [Header("Bullet Properties")]
   
    [Range(10,50)]
    public int playerBulletNumber = 50;
    public int playerBulletCount = 0;
    public int playerActiveBullets = 0;

    [Range(10, 50)]
    public int enemyBulletNumber = 50;
    public int enemyBulletCount = 0;
    public int enemyActiveBullets = 0;

    private BulletFactory factory;
    private Queue<GameObject> playerBulletPool;
    private Queue<GameObject> enemyBulletPool;
   

    void Start()
    {
        playerBulletPool = new Queue<GameObject>();
        enemyBulletPool = new Queue<GameObject>();
        factory = GameObject.FindObjectOfType<BulletFactory>(); 

        BuildBulletPools();


    }

    private void BuildBulletPools()
    {

        for (int i = 0; i < playerBulletNumber; i++)
        {
            playerBulletPool.Enqueue(factory.CreateBullet(BulletType.PLAYER));
        }

        for (int i = 0; i < enemyBulletNumber; i++)
        {
            enemyBulletPool.Enqueue(factory.CreateBullet(BulletType.ENEMY));
        }

        playerBulletCount = playerBulletPool.Count;
        enemyBulletCount = enemyBulletPool.Count;
    }

/*    private void CreateBullet()
    {
        *//*var bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        bullet.transform.SetParent(bulletParent);*//*

 
        playerBulletPool.Enqueue(factory.CreateBullet(BulletType.PLAYER));
        enemyBulletPool.Enqueue(factory.CreateBullet(BulletType.ENEMY));
    }*/

    public GameObject GetBullet(Vector2 position, BulletType type)
    {
    

        GameObject bullet = null;

        switch(type)
        {
            case BulletType.PLAYER:
                {
                    if (playerBulletPool.Count < 1)
                    {
                        playerBulletPool.Enqueue(factory.CreateBullet(BulletType.PLAYER));
                    }
                    bullet = playerBulletPool.Dequeue();
                    playerActiveBullets++;
                    playerBulletCount = playerBulletPool.Count;
                }
                break;
            case BulletType.ENEMY:
                {
                    if (enemyBulletPool.Count < 1)
                    {
                        enemyBulletPool.Enqueue(factory.CreateBullet(BulletType.ENEMY));
                    }
                    bullet = enemyBulletPool.Dequeue();
                    enemyActiveBullets++;
                    enemyBulletCount = enemyBulletPool.Count;
                }
                break;
        }

      


        bullet.SetActive(true);
        bullet.transform.position = position;

        return bullet;

    }

    public void ReturnBullet(GameObject bullet, BulletType type)
    {
        bullet.SetActive(false);

        switch(type)
        {
            case BulletType.PLAYER:
                {
                    playerBulletPool.Enqueue(bullet);

                    //stats
                    playerActiveBullets--;
                    playerBulletCount = playerBulletPool.Count;
                }
                break;
            case BulletType.ENEMY:
                {
                    enemyBulletPool.Enqueue(bullet);

                    //stats
                    enemyActiveBullets--;
                    enemyBulletCount = enemyBulletPool.Count;
                }
                break;
        }
   
    }

}
