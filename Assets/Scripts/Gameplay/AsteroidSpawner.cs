using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    [SerializeField] GameObject prefabAsteroid;

	// Use this for initialization
	void Start () {

        InstantiateAsteroid(Direction.Left, new Vector2(ScreenUtils.ScreenRight, 0));
        InstantiateAsteroid(Direction.Right, new Vector2(ScreenUtils.ScreenLeft, 0));
        InstantiateAsteroid(Direction.Up, new Vector2(0, ScreenUtils.ScreenBottom));
        InstantiateAsteroid(Direction.Down, new Vector2(0, ScreenUtils.ScreenTop));

    }
	

    void InstantiateAsteroid(Direction direction, Vector2 location) {

        GameObject asteroid = Instantiate<GameObject>(prefabAsteroid, location, Quaternion.identity);
        float radius = asteroid.GetComponent<CircleCollider2D>().radius;
        asteroid.transform.position = new Vector2(location.x - radius, location.y - radius);
        asteroid.GetComponent<Asteroid>().Initialize(direction);
    }
}
