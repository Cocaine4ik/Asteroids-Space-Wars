using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    [SerializeField] Text aliveTimeText;

    int aliveTime = 0;
    float elapedSeconds = 0;
    bool timerRun = true;

	// Use this for initialization
	void Start () {

        aliveTimeText.text = "Alive time: " + aliveTime.ToString();
	}
	
	// Update is called once per frame
	void Update () {

        ShowAliveTime();

    }

    public void StopGameTimer() {
        timerRun = false;
    }
    void ShowAliveTime() {
        if (timerRun) {
            elapedSeconds += Time.deltaTime;
            aliveTime = (int)elapedSeconds;
            aliveTimeText.text = "Alive time: " + aliveTime.ToString();
        }
    }
}
