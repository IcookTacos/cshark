using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public List<string> gameState;

    private Canvas startScreenCanvas;

    private Rigidbody playerRB;

    public Camera launchCamera;
    public Camera upgradeCamera;

    public Vector3 upgradePosition;
    public Vector3 launchPosition;
    public Vector3 endPosition;

    private Quaternion originalRotation;

    public float velocity_Y;
    public float velocity_Z;

    public float steerSpeed;
    public float rotation;
    public float translation;

    // Temporary flag to avoid multiple ground detections, find fix!
    public bool flag;
    public bool endOfTheWorld;

    public int gold;
    public int coinsCollected;

    private int gameStateIndex;

    public bool startScreen;

    public int gunpowderLVL;
    public int gunpowderMaxLVL;
    public int[] gunpowderCost;
    public float[] gunpowderUpgrade;

    public int cannonLVL;
    public int cannonMaxLVL;
    public int[] cannonCost;
    public float[] cannonUpgrade;

    public int flightLVL;
    public int flightMaxLVL;
    public int[] flightCost;
    public float[] flightUpgrade;

    void Start(){
        setFlightCameraMode();
        initializePlayer();
    }

    void Update(){
        if(this.transform.position.z>750f) endOfTheWorld = true;
    }

    public void initializePlayer(){

        startScreen = true;

        gameState       = new List<string> {"launch","flight","land","upgrade"};
        upgradePosition = new Vector3(-2,1,5);

        playerRB = this.GetComponent<Rigidbody>();

        startScreenCanvas = GameObject.FindGameObjectWithTag("StartScreenUI").GetComponent<Canvas>();

        flag = true;
        endOfTheWorld = false;

        cannonUpgrade = new float[5];
        cannonCost = new int[5];

        flightUpgrade = new float[6];
        flightCost = new int[6];

        gunpowderUpgrade = new float[4];
        gunpowderCost = new int[4];

        gameStateIndex  = 0;
        gold            = 0;
        coinsCollected  = 0;

        cannonLVL       = 0;
        flightLVL       = 0;
        gunpowderLVL    = 0;

        cannonMaxLVL    = 4;
        flightMaxLVL    = 5;
        gunpowderMaxLVL = 2;
 
        upgradeCamera.enabled   = false;
        launchCamera.enabled    = true;

        velocity_Y = 500f;
        velocity_Z = 500f;

        steerSpeed = 4f;

        originalRotation = this.transform.rotation;

        initializeCannonStats();
        initializeFlightStats();
        initializeGunpowderStats();

   }

   private void initializeCannonStats(){

       cannonCost[0] = 50;
       cannonCost[1] = 125;
       cannonCost[2] = 450;
       cannonCost[3] = 750;
       cannonCost[4] = 850;

       cannonUpgrade[0] = 300f;
       cannonUpgrade[1] = 450f;
       cannonUpgrade[2] = 650f;
       cannonUpgrade[3] = 950f;
       cannonUpgrade[4] = 1100f;
       
   }

   private void initializeFlightStats(){

       flightCost[0] = 25;
       flightCost[1] = 50;
       flightCost[2] = 125;
       flightCost[3] = 550;
       flightCost[4] = 650;
       flightCost[5] = 750;

       flightUpgrade[0] = 300f;
       flightUpgrade[1] = 350f;
       flightUpgrade[2] = 300f;
       flightUpgrade[4] = 350f;
       flightUpgrade[5] = 400f;
   }

   private void initializeGunpowderStats(){

       gunpowderCost[0] = 350;
       gunpowderCost[1] = 750;
       gunpowderCost[2] = 1100;
       gunpowderCost[3] = 1050;

       gunpowderUpgrade[0] = 0.9f;
       gunpowderUpgrade[1] = 0.85f;
       gunpowderUpgrade[2] = 0.75f;
       gunpowderUpgrade[3] = 0.72f;

   }

    public void setFlightCameraMode(){
        upgradeCamera.enabled = false;
        launchCamera.enabled = true;
    }

    public void setUpgradeCameraMode(){
        upgradeCamera.enabled = true;
        launchCamera.enabled = false;
    }

    public string getGameState(){
        return gameState[gameStateIndex];
    }

    public void nextGameState(){
        gameStateIndex++;
        gameStateIndex %= gameState.Count;
    }

    public int getCannonCost(){
        return cannonCost[cannonLVL];
    }

    public int getFlightCost(){
        return flightCost[flightLVL];
    }

    public int getGunpowderCost(){
        return gunpowderCost[gunpowderLVL];
    }

    public void upgradeFlight(){
        if(flightLVL==flightCost.Length-1) return;
        velocity_Y += flightUpgrade[flightLVL];
        steerSpeed += 1f;
        gold -= getFlightCost();
        flightLVL += 1;
    }

    public void upgradeCannon(){
        if(cannonLVL==cannonCost.Length-1) return; 
        velocity_Z += cannonUpgrade[cannonLVL];
        steerSpeed += 1f;
        gold -= getCannonCost();
        cannonLVL += 1;
    }

    public void upgradeGunpowder(){
        if(gunpowderLVL==gunpowderCost.Length-1) return;
        playerRB.mass = gunpowderUpgrade[gunpowderLVL];
        steerSpeed += 2f;
        gold -= getGunpowderCost();
        gunpowderLVL += 1;
    }

    public void steer(){
        if(this.transform.position.y<=3.5f) return;
        translation = Input.GetAxis("Horizontal") * steerSpeed;
        rotation = Input.GetAxis("Horizontal");

        rotation *= Time.deltaTime;
        translation *= Time.deltaTime;
        transform.Translate(translation,0,0); 
        transform.Rotate(0,rotation,0); 
    }

    public void steerNormal(){
        this.transform.rotation = originalRotation;
    }

    public void steerDown(){
        if(this.transform.position.y<=5f) return;
        if(playerRB.velocity.magnitude == 0) return;
        float y = -10f;
        float z = -10f;
        float x = 0;
        playerRB.AddRelativeForce(new Vector3(x,y,z));
    }

    public void collectCoin(){ coinsCollected += 1; }
    public void resetCoins(){ coinsCollected = 0; }
    public int getCoinsCollected(){ return coinsCollected; }

    public void exitStartScreen(){
        startScreenCanvas.enabled = false;
        startScreen = false;
    }
}
