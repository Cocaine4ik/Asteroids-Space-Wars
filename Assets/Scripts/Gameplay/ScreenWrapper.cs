using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour {

    float radius;

    // Use this for initialization
    void Start () {

        radius = GetComponent<CircleCollider2D>().radius;

    }

    void OnBecameInvisible() {
        Vector2 position = transform.position;

        // check left, right, top, and bottom sides
        if (position.x + radius < ScreenUtils.ScreenLeft ||
            position.x - radius > ScreenUtils.ScreenRight) {
            position.x *= -1;
        }
        if (position.y - radius > ScreenUtils.ScreenTop ||
            position.y + radius < ScreenUtils.ScreenBottom) {
            position.y *= -1;
        }

        // move ship
        transform.position = position;
    }
}
