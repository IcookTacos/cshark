using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{

    public GameObject player;
    public GameObject playerSuper;
    public Camera endOfWorldCamera;

    bool spaceBarEnabled;

    LaunchScript launchScript;

    UpgradeMenu upgradeMenu;

    StatsUI statsUI;

    float distanceScore;
    float cooldown;

    PlayerStats playerStats;

    Vector3 spawnPosition;

    Quaternion playerOriginalRotation;

    void Start(){
        
        spaceBarEnabled = true;

        statsUI = GameObject.FindGameObjectWithTag("StatsUI").GetComponent<StatsUI>();
        upgradeMenu  = GameObject.FindGameObjectWithTag("UpgradeUI").GetComponent<UpgradeMenu>();
        playerStats  = GetComponent<PlayerStats>();
        launchScript = GetComponent<LaunchScript>();

        endOfWorldCamera.enabled = false;

        spawnPosition = player.transform.position; 
        playerOriginalRotation = player.transform.rotation;

        cooldown = 1.25f;

    }

    void Update(){
        if(!playerStats.endOfTheWorld){
            if(Input.GetKeyUp(KeyCode.Space)    && spaceBarEnabled && !playerStats.startScreen) spaceAction();
            if(Input.GetKey(KeyCode.DownArrow)) playerStats.steerDown();
            if(Input.GetKeyUp(KeyCode.Space) && playerStats.startScreen) playerStats.exitStartScreen();
            playerStats.steer();
        }
        if(playerStats.endOfTheWorld) endScreen();

    }    

    private void endScreen(){
        Debug.Log("Victory!!");
        statsUI.disableUI();
        endOfWorldCamera.enabled = true;
        Destroy(playerSuper);
    }

    private void spaceAction(){
        spaceBarEnabled = false;
        StartCoroutine(spaceBarCooldown(cooldown));
        generateGameState();
    }

    private void respawnPlayerInUpgradeMode(){
        statsUI.disableUI();
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.rotation = playerOriginalRotation;
        player.transform.position = playerStats.upgradePosition;
        upgradeMenu.enableMenu(true);
        playerStats.setUpgradeCameraMode();
        StartCoroutine(changeStateCooldown(cooldown));
    }

    private void respawnPlayerInCannonMode(){
        upgradeMenu.enableMenu(false);
        player.transform.position = spawnPosition;
        playerStats.setFlightCameraMode();
        StartCoroutine(changeStateCooldown(cooldown));
    }

    public int calculateLaunchScore(Vector3 end){
        Vector3 distance = spawnPosition - end;
        distanceScore = distance.magnitude;
        return (int)distanceScore;
    }

    public int addCoinsToPlayer(){
        float goldEarned = distanceScore * 0.70f;
        goldEarned += playerStats.getCoinsCollected() * 50;
        playerStats.gold += (int)goldEarned;
        return (int) goldEarned;
    }

    private void generateGameState(){
        if(playerStats.getGameState() == "upgrade") respawnPlayerInCannonMode();
        if(playerStats.getGameState() == "launch")  launchPlayer();
        if(playerStats.getGameState() == "land")    respawnPlayerInUpgradeMode();
    }

    private void launchPlayer(){
        playerStats.flag = true; //TODO: Fix this stupid shit
        launchScript.launchPlayer();
        StartCoroutine(changeStateCooldown(cooldown));
    }

    // Setting a small cooldown on chaning state helps avoid buggy interactions between rapid state changes
    IEnumerator changeStateCooldown(float cooldown){
        yield return new WaitForSeconds(cooldown);
        playerStats.nextGameState();
    }

    // Setting a small cooldown on spacebar helps avoid buggy interactions with rapid buttonpressing
    IEnumerator spaceBarCooldown(float cooldown){
        yield return new WaitForSeconds(cooldown);
        spaceBarEnabled = true;
    }

}