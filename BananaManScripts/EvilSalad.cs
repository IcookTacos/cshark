using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSalad : MonoBehaviour{

    public GameObject player;
    private Transform playerTransform;
    private float speed;

    void Awake(){
        speed = Random.Range(2,15);
    }

    void Start(){
        playerTransform = player.transform;
    }

    void Update(){
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer(){
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,playerTransform.position,step);

    }

    void OnTriggerEnter(Collider c){
        Debug.Log(c.name);
        if(c.name=="PlayerArmature"){
            PlayerStats playerStats = c.gameObject.GetComponent<PlayerStats>();
            playerStats.ResetStats();
            playerStats.ResetBananas();
            FindObjectOfType<AudioManager>().PlaySound("GetHit");
            Destroy(gameObject);
        }
    }

}
