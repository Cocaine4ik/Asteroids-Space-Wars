using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    [SerializeField] Sprite[] asteroidSprites = new Sprite[3];
    Rigidbody2D rb;
    CircleCollider2D circleCollider2D;

    const float MinSpeed = 1;
    const float MaxSpeed = 2;

    void Start () {

        GetComponent<SpriteRenderer>().sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];

        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void Initialize(Direction direction) {

        float angle = 0;

        switch (direction) {
            case Direction.Left: angle = Random.Range(165, 195); break;
            case Direction.Right: angle = Random.Range(345, 375); break;
            case Direction.Up: angle = Random.Range(75, 105); break;
            case Direction.Down: angle = Random.Range(255, 285); break;

            default: Debug.Log("Wrong direction!"); break;
        }
        // add force-impulse with random speed to random direction
        StartMoving(angle);
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Bullet") {
            Destroy(collision.gameObject);

            if(gameObject.transform.localScale.x > 0.5) {
                SplitAsteroid();
            }
            // play audio
            AudioManager.Play(AudioClipName.AsteroidHit);
            Destroy(this.gameObject);
        }
    }

    private void SplitAsteroid() {

        Vector2 newScale = new Vector2(gameObject.transform.localScale.x / 2, gameObject.transform.localScale.y / 2);
        float newRadius = circleCollider2D.radius / 2;

        gameObject.transform.localScale = newScale;
        circleCollider2D.radius = newRadius;

        for(int i = 0; i < 2; i++) {

            GameObject smallAsteroid = Instantiate(gameObject);
            smallAsteroid.GetComponent<Asteroid>().StartMoving(Random.Range(0, 360));
        }


    }
    private void StartMoving(float angle) {

        rb = GetComponent<Rigidbody2D>();

        Vector2 moveDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        rb.AddForce(moveDirection * Random.Range(MinSpeed, MaxSpeed), ForceMode2D.Impulse);
    }
}
