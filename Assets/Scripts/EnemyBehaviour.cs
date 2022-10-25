/*--------------------Space Madness----------------------------------
 * -----------a space shooter game with chaotic atmosphere-----------
 * Name: Sinan Kolip
 * Student Number: 101312965
 * Last Modified Time: 10/24/2022
 * Enemy movement, spawn position and attack feature
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Boundary horizontalBoundry;
    public Boundary verticalBoundry;
    public Boundary screenBounds;
    public float horizontalSpeed, verticalSpeed;
    [SerializeField]
    private GameObject _explosion;
    [Header("Bullet Properties")]
    public Transform bulletSpawnPoint;
    public float fireRate = 0.2f;

    public AudioClip _shootingClip, _explosionClip;
    AudioSource audioSource;

    bool isDestroyed = false;
    private SpriteRenderer spriteRenderer;
    private BulletManager bulletManager;


    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();

        audioSource = GetComponent<AudioSource>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        ResetEnemy();

        InvokeRepeating("FireBullets", 0.0f, fireRate);
    }

    // Update is called once per frame,
    void Update()
    {

        Move();
        CheckBounds();
    }

    void Move()
    {
        var horizontalLenght = horizontalBoundry.max - horizontalBoundry.min;
        transform.position = new Vector3(Mathf.PingPong(Time.time * horizontalSpeed, horizontalLenght) -
            horizontalBoundry.max, transform.position.y - verticalSpeed * Time.deltaTime, transform.position.z);
    }
    void FireBullets()
    {
        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.ENEMY);
        audioSource.Play();
    }
    private void CheckBounds()
    {
        if (transform.position.y < screenBounds.min)
            ResetEnemy();
    }
    private void ResetEnemy()
    {
        var RandomXPosition = Random.RandomRange(horizontalBoundry.min, horizontalBoundry.max);
        var RandomYPosition = Random.RandomRange(verticalBoundry.min, verticalBoundry.max);

        horizontalSpeed = Random.Range(1.0f, 6.0f);
        verticalSpeed = Random.Range(1f, 3f);

        transform.position = new Vector3(RandomXPosition, RandomYPosition, 0);

        List<Color> colorList = new List<Color>() { Color.red, Color.yellow, Color.magenta, Color.cyan, Color.white, Color.white};

        var randomColor = colorList[Random.Range(0, 6)];
        spriteRenderer.material.SetColor("_Color", randomColor);
    
    }

    public IEnumerator ExplosionCoroutine()
    {
        if (isDestroyed || transform.position.y > 5)
            yield break;
        isDestroyed = true;
       

        audioSource.clip = _explosionClip;
        audioSource.Play();

        CancelInvoke();
        spriteRenderer.enabled = false;
        _explosion.SetActive(true);

        FindObjectOfType<ScoreManager>().AddPoint(15);

        this.enabled = false;
        yield return new WaitForSeconds(1);
        FindObjectOfType<SpawnManager>().enemyDestroyed();

        Destroy(this.gameObject);
    }
}
