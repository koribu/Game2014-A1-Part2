/*--------------------Space Madness----------------------------------
 * -----------a space shooter game with chaotic atmosphere-----------
 * Name: Sinan Kolip
 * Student Number: 101312965
 * Last Modified Time: 10/24/2022
 * Bullet behaviors for enemy and player types
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//using UnityEngine.AdaptivePerformance.VisualScripting;

[System.Serializable]
public struct ScreenBounds
{
    public Boundary horizontal;
    public Boundary vertical;
}

public class BulletBehaviour : MonoBehaviour
{
    [Header("Bullet Properties")]
    public BulletDirection bulletDirection;
    public float speed;
    public ScreenBounds bounds;
    public BulletType bulletType;

    private BulletManager bulletManager;
    private Vector3 velocity;
    private ScoreManager scoreManager;


    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        bulletManager = FindObjectOfType<BulletManager>();
    }


    private void Update()
    {
        Move();
        CheckBounds();
    }

    private void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }

    void CheckBounds()
    {
        if(transform.position.x > bounds.horizontal.max || 
            transform.position.x<bounds.horizontal.min ||
            transform.position.y>bounds.vertical.max ||
            transform.position.y<bounds.vertical.min)
        {
            bulletManager.ReturnBullet(this.gameObject, bulletType);
        }
    }

    public void SetDirection(float angle)
    {
        Vector3 direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle),0);
        velocity = direction * speed;
        /* switch (direction)
         {
             case BulletDirection.UP:
                 velocity = Vector3.up * speed;
                 break;
             case BulletDirection.RIGHT:
                 velocity = Vector3.right * speed;
                 break;
             case BulletDirection.LEFT:
                 velocity = Vector3.left * speed;
                 break;
             case BulletDirection.DOWN:
                 velocity = Vector3.down * speed;
                 break;
         }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(this.gameObject);
        if(bulletType == BulletType.PLAYER && collision.CompareTag("Enemy"))
        {
            bulletManager.ReturnBullet(this.gameObject, bulletType);
            collision.GetComponent<EnemyBehaviour>().StartCoroutine("ExplosionCoroutine");
        }
        else if (bulletType == BulletType.PLAYER && collision.CompareTag("Asteroid"))
        {
            bulletManager.ReturnBullet(this.gameObject, bulletType);
            collision.GetComponent<AsteroidBehaviour>().StartCoroutine("ExplosionCoroutine");
        }
        else if (bulletType == BulletType.ENEMY && collision.gameObject.CompareTag("Player"))
        {
            bulletManager.ReturnBullet(this.gameObject, bulletType);
            scoreManager.getHit(15);
            //collision.GetComponent<Player>().StartCoroutine("ExplosionCoroutine");

        }
       
    }
}