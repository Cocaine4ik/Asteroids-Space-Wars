using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    // for movement and rotation
    Vector2 thrustDirection = new Vector2(1, 0);
    const float ThrustForce = 5;
    const float DegreesPerSecond = 180;
    Rigidbody2D rb;

    // for shooting
    [SerializeField] GameObject prefabBullet;

    //temporary for HUD
    [SerializeField] GameObject HUD;

	void Start () {

        rb = GetComponent<Rigidbody2D>();

	}
	
	void Update () {

        float rotationInput = Input.GetAxis("Rotate");
        if (rotationInput != 0) {

            // calculate rotation amount and apply rotation
            float rotationAmount = DegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0) {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            // change thrust direction to match ship rotation
            float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(zRotation);
            thrustDirection.y = Mathf.Sin(zRotation);
        }
        // shooting bullets
        if(Input.GetKeyDown(KeyCode.LeftControl)) {

            // play shot audio
            AudioManager.Play(AudioClipName.PlayerShot);

            // ship position
            Vector2 position = transform.position;
            // instantiate new bullet with ship postion
            GameObject bullet = Instantiate(prefabBullet, new Vector2(position.x, position.y),
                Quaternion.identity);
            // rotate the bullet as the ship rotation
            bullet.transform.Rotate(transform.rotation.eulerAngles);
            // apply force to the bullet
            bullet.GetComponent<Bullet>().ApplyForce(thrustDirection);
        }
    }
    void FixedUpdate() {
        // add trhust force to move the ship
        if (Input.GetAxis("Thrust") != 0) {
            rb.AddForce(thrustDirection * ThrustForce, ForceMode2D.Force);
            // play audio
            // AudioManager.Play(AudioClipName.ShipThrust, 0.02f);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Asteroid") {
            Destroy(this.gameObject);
            // play audio
            AudioManager.Play(AudioClipName.PlayerDeath);
            // stop the timer
            HUD.GetComponent<HUD>().StopGameTimer();
        }
    }
}
