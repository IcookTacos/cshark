using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    public Vector3 offset;

    void Start(){
        offset = new Vector3(0,2.5f,-8);
    }

    void Update(){

        this.transform.position = player.transform.position + offset;
        this.transform.LookAt(player.transform);

    }
}
