using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    PlayerStats playerStats;
    Animator playerAnimator;
    TrailRenderer trail;
    GameController gameController;
    StatsUI statsUI;
    AudioController audioController;

    int distance;
    int coins;

    float cooldown = 1.25f;

    void Start(){
        playerStats = GetComponent<PlayerStats>();
        gameController = GetComponent<GameController>();
        playerAnimator = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<Animator>();
        trail = GameObject.FindGameObjectWithTag("Trail").GetComponent<TrailRenderer>();
        playerAnimator.ResetTrigger("PlayerLand");
        statsUI = GameObject.FindGameObjectWithTag("StatsUI").GetComponent<StatsUI>();
        audioController = GetComponent<AudioController>();
    }

    private void OnCollisionEnter(Collision col){
        if(playerStats.flag && playerStats.getGameState() == "flight") land();
    }

    private void land(){
        playerStats.flag = false; // TODO: Fix this stupid shit
        playerAnimator.SetTrigger("PlayerLand");
        playerStats.endPosition = this.transform.position;
        playerStats.steerNormal();
        audioController.landSound();
        distance = gameController.calculateLaunchScore(playerStats.endPosition);
        coins = gameController.addCoinsToPlayer();
        StartCoroutine(disableTrail());
        StartCoroutine(changeStateCooldown(cooldown));
    }

    // Having a small cooldown on state change prevents buggy beahvior with rapid state changes
    IEnumerator changeStateCooldown(float cooldown){
        yield return new WaitForSeconds(cooldown);
        statsUI.enableUI(distance,coins);
        playerStats.nextGameState();
    }

    // We wait for the trail to complete rendering before disabling it
    IEnumerator disableTrail(){
        yield return new WaitForSeconds(0.75f);
        trail.enabled = false;
    }

}
