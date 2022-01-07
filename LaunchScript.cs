using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchScript : MonoBehaviour{
    GameController gameController;
    ParticleSystem explosion;
    Animator playerAnimator;
    TrailRenderer trail;
    AudioController audioController;

    public PlayerStats playerStats;
    float multipler;

    void Start(){
        explosion = GameObject.FindGameObjectWithTag("Cannon").GetComponent<ParticleSystem>();
        explosion.Stop();
        playerAnimator = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<Animator>();
        trail = GameObject.FindGameObjectWithTag("Trail").GetComponent<TrailRenderer>();
        gameController = GetComponent<GameController>();
        playerStats = GetComponent<PlayerStats>();
        audioController = GetComponent<AudioController>();

    }

    public void launchPlayer(){
        playerAnimator.SetTrigger("PlayerFlying");
        trail.enabled = true;
        explosion.Play();
        audioController.cannonSound();
        audioController.flyingSound();
        playerStats.resetCoins();
        multipler = randomMultiplier();
        float y = playerStats.velocity_Y + multipler;
        float z = playerStats.velocity_Z + multipler;
        float x = 0;
        this.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(x,y,z)); 
    }

    private float randomMultiplier(){
        return 100f * Random.Range(0,4);
    }

}
