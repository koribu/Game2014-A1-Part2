using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletFactory : MonoBehaviour
{
    //Bullet prefab
    public GameObject bulletPrefab;

    //Sprite Textures
    public Sprite playerBulletSprite;
    public Sprite enemyBulletSprite;
    [Range(0,360)]
    public float playerBulletAngle;
    //Bullet Parent
    public Transform bulletParent;
    
    // Start is called before the first frame update
    void Start()
    {
        //Initialize(); // Commend outed since the load doesnt work properly
    }

    private void Initialize()
    {
        playerBulletSprite = Resources.Load<Sprite>("Sprites/Bullet");
        enemyBulletSprite = Resources.Load<Sprite>("Sprites/EnemySmallBullet"); /*as Sprite*/
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletParent = GameObject.Find("Bullets").transform;
    }

    public GameObject CreateBullet(BulletType type)
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);
        bullet.GetComponent<BulletBehaviour>().bulletType = type;

        switch (type)
        {
            case BulletType.PLAYER:
                {
                    bullet.GetComponent<SpriteRenderer>().sprite = playerBulletSprite;
                    bullet.GetComponent<BulletBehaviour>().SetDirection(playerBulletAngle);
                    bullet.name = "PlayerBullet";
                }
                break;
            case BulletType.ENEMY:
                {
                    bullet.GetComponent<SpriteRenderer>().sprite = playerBulletSprite;
                    bullet.GetComponent<SpriteRenderer>().color = Color.cyan;
                    bullet.GetComponent<BulletBehaviour>().SetDirection(180);
                    bullet.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                    bullet.name = "EnemyBullet";
                }
                break;
          
        }

        bullet.SetActive(false);
        return bullet;
    }
}
