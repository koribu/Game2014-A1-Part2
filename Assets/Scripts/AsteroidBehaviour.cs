/*--------------------Space Madness----------------------------------
 * -----------a space shooter game with chaotic atmosphere-----------
 * Name: Sinan Kolip
 * Student Number: 101312965
 * Last Modified Time: 10/24/2022
 * Asteroid behaviors such as movement and damage to player
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    public Boundary horizontalBoundry;
    public Boundary verticalBoundry;
    public Boundary screenBounds;
    public float horizontalSpeed, verticalSpeed;
    [SerializeField]
    private GameObject _explosion;

    private SpriteRenderer spriteRenderer;

    private float rotationSpeed;
    private bool isDestroyed;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Mathf.Deg2Rad * Random.Range(-2, 2); 
        spriteRenderer = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();

        ResetAsteroid();

    }

    // Update is called once per frame,
    void Update()
    {

        Move();
        CheckBounds();
    }

    void Move()
    {
        transform.RotateAround(Vector3.forward, rotationSpeed);
        var horizontalLenght = horizontalBoundry.max - horizontalBoundry.min;
        transform.position = new Vector3(Mathf.PingPong(Time.time * horizontalSpeed, horizontalLenght) -
            horizontalBoundry.max, transform.position.y - verticalSpeed * Time.deltaTime, transform.position.z);
    }
 
    private void CheckBounds()
    {
        if (transform.position.y < screenBounds.min)
            ResetAsteroid();
    }
    private void ResetAsteroid()
    {
        var RandomXPosition = Random.RandomRange(horizontalBoundry.min, horizontalBoundry.max);
        var RandomYPosition = Random.RandomRange(verticalBoundry.min, verticalBoundry.max);

        horizontalSpeed = Random.Range(0.2f, 3.0f);
        verticalSpeed = Random.Range(1f, 3f);

        transform.position = new Vector3(RandomXPosition, RandomYPosition, 0);

        transform.localScale = new Vector3(Random.Range(0.5f, 0.8f), Random.Range(0.5f, 0.8f), Random.Range(0.5f, 0.8f));

    }

    public IEnumerator ExplosionCoroutine()
    {
        if (isDestroyed || transform.position.y > screenBounds.max)
            yield break;
        isDestroyed = true;
        this.enabled = false;

        audioSource.Play();

        spriteRenderer.enabled = false;
        _explosion.SetActive(true);

        FindObjectOfType<ScoreManager>().AddPoint(10);

        yield return new WaitForSeconds(1);
        FindObjectOfType<SpawnManager>().asteroidDestroyed();
        Destroy(gameObject);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ExplosionCoroutine());
        
            FindObjectOfType<ScoreManager>().getHit(25);
        }
    }


}
