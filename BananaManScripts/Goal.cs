using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour{

    public GameObject meshPrefab;

    public void SetPosition(int x, int y, int z){
        this.transform.position = new Vector3(x,y,z);
    }

    void OnTriggerEnter(Collider c){
        if(c.name=="PlayerArmature"){
            FindObjectOfType<AudioManager>().PlaySound("StageClear");
            c.GetComponent<PlayerStats>().RespawnPlayer();
            c.GetComponent<PlayerStats>().ResetStats();
            meshPrefab.GetComponent<MeshGenerator>().GenerateNewStage();
        }
    }
       
}
