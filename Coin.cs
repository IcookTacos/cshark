using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour{

    private float rotationSpeed;
    PlayerStats playerStats;
    AudioController audioController;

    void Start(){   
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        audioController = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioController>();
        rotationSpeed = 2f;  
    }

    void Update(){
       this.transform.Rotate(0,rotationSpeed,0); 
    }

    private void OnTriggerEnter(Collider other){
        audioController.coinSound();
        playerStats.collectCoin();
        Destroy(gameObject);
    }

}
