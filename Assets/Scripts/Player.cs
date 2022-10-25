/*--------------------Space Madness----------------------------------
 * -----------a space shooter game with chaotic atmosphere-----------
 * Name: Sinan Kolip
 * Student Number: 101312965
 * Last Modified Time: 10/24/2022
 * Player behaviours such as triggers, movement and shooting
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField]
    float m_speed;
    public Boundary boundaryX, boundaryY;
    public float verticalPosition;

    public float speed = 10.0f;
    public bool usingMobileInput = false;

    [SerializeField]
    GameObject _explosion;

    [Header("Bullet Properties")]
    public Transform bulletSpawnPoint;
    public Transform tripleBulletSpawnPointL, tripleBulletSpawnPointR;
    public float fireRate = 0.2f;

    private BulletManager bulletManager;
    private Camera camera;
    private ScoreManager scoreManager;
    private AudioSource audioSource;
    public AudioClip _explosionClip;
    private bool _powerUpActivated;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        bulletManager = FindObjectOfType<BulletManager>();
        camera = Camera.main;

        /*   if (Application.platform != RuntimePlatform.Android &&
               Application.platform != RuntimePlatform.IPhonePlayer)
           {
               usingMobileInput = false;
           }
           else
           {
               usingMobileInput = true;
           }*/
        usingMobileInput = Application.platform == RuntimePlatform.Android ||
               Application.platform == RuntimePlatform.IPhonePlayer;

        scoreManager = FindObjectOfType<ScoreManager>();


        InvokeRepeating("FireBullets", 0.0f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
     
        if (usingMobileInput)
            MobileInput();
        else
            ConventionalInput();
        
        Move();

        if(Input.GetKeyDown(KeyCode.K))
        {
            scoreManager.AddPoint(7);
        }


        // transform.Translate(new Vector2(x * m_speed, y * m_speed));
    }

    private void MobileInput()
    {
        foreach(var touch in Input.touches)
        {
            Vector2 destination = new Vector2(camera.ScreenToWorldPoint(touch.position).x, camera.ScreenToWorldPoint(touch.position).y);
            Debug.Log(destination);
           transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * speed);
        }

    }

    void SpawnBorder()
    {
        if (transform.position.x >= 3)
            transform.position = new Vector2(-3, transform.position.y);
        else if (transform.position.x < -3)
            transform.position = new Vector2(3, transform.position.y);
    }

    public void ConventionalInput()
    {
        float x = Input.GetAxisRaw("Horizontal") * m_speed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * m_speed * Time.deltaTime;
        Vector3 newPos = new Vector3(x + transform.position.x, y + transform.position.y);
        transform.position = newPos;
    }
    void Move()
    {
        SpawnBorder();

        CheckBounds();


    }

    void FireBullets()
    {
        if (!_powerUpActivated)
        {
            var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.PLAYER);
        }
        else
        {
            var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.PLAYER);
            bullet = bulletManager.GetBullet(tripleBulletSpawnPointL.position, BulletType.PLAYER);
            bullet = bulletManager.GetBullet(tripleBulletSpawnPointR.position, BulletType.PLAYER);
        }

        audioSource.Play();
    }

    void CheckBounds()
    {

        if (transform.position.y > boundaryY.max)
        {
            transform.position = new Vector2(transform.position.x, boundaryY.max);
        }
        if (transform.position.y < boundaryY.min)
        {
            transform.position = new Vector2(transform.position.x, boundaryY.min);
        }

        /*    float clampedPosition = Mathf.Clamp(transform.position.x, boundary.min, boundary.max);
            transform.position = new Vector2(clampedPosition, verticalPosition);*/

        if (transform.position.x > boundaryX.max)
        {
            transform.position = new Vector2(boundaryX.max, transform.position.y);
        }
        if (transform.position.x < boundaryX.min)
        {
            transform.position = new Vector2(boundaryX.min, transform.position.y);
        }
    }

    public IEnumerator ExplosionCoroutine()
    {
        
        CancelInvoke();
        GetComponent<SpriteRenderer>().enabled = false;
        _explosion.SetActive(true);
        audioSource.clip = _explosionClip;
        audioSource.Play();
        GameObject.Find("GameController").GetComponent<GameController>().GameOver();
        this.enabled = false;
        yield return new WaitForSeconds(2);

      

        Destroy(this.gameObject);
    }

    public IEnumerator TripleShotCoroutine()
    {
        _powerUpActivated = true;
        yield return new WaitForSeconds(8);
        _powerUpActivated = false;
    }
}
