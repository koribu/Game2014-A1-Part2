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

    private SpriteRenderer spriteRenderer;
    private BulletManager bulletManager;
    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();

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
        this.enabled = false;
        spriteRenderer.enabled = false;
        _explosion.SetActive(true);
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
