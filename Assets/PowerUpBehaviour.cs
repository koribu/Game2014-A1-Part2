using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public Boundary horizontalBoundry;
    public Boundary verticalBoundry;
    public Boundary screenBounds;
    public float horizontalSpeed, verticalSpeed;
    [SerializeField]
    private GameObject _explosion;


    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        var RandomXPosition = Random.RandomRange(horizontalBoundry.min, horizontalBoundry.max);
        var RandomYPosition = Random.RandomRange(verticalBoundry.min, verticalBoundry.max);

        horizontalSpeed = Random.Range(0.2f, 3.0f);
        verticalSpeed = Random.Range(1f, 3f);

        transform.position = new Vector3(RandomXPosition, RandomYPosition, 0);


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

    private void CheckBounds()
    {
        if (transform.position.y < screenBounds.min)
            Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().StartCoroutine("TripleShotCoroutine");
            Destroy(gameObject);
        }
    }

}
