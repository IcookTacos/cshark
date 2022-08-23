using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour{

    void OnTriggerEnter(Collider c){
        if(c.name=="PlayerArmature"){
            PlayerStats playerStats = c.gameObject.GetComponent<PlayerStats>();
            playerStats.UpgradeStats();
            playerStats.CollectBanana(1);
            FindObjectOfType<AudioManager>().PlaySound("PowerUp");
            Destroy(gameObject);
        }
   } 

}
