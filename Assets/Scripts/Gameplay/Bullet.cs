using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // for timer
    const float aliveTime = 2f;
    Timer timer;

    private void Start() {
        // create new timer
        timer = GetComponent<Timer>();
        timer.Duration = aliveTime;
        timer.Run();
    }

    private void Update() {
        if(timer.Finished) {
            Destroy(gameObject);
        }
    }
    public void ApplyForce (Vector2 direction) {
        const float magnitude = 5f;
        GetComponent<Rigidbody2D>().AddForce(direction * magnitude, ForceMode2D.Impulse);
    }
}
