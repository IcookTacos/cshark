using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour{

    private float rotationSpeed;
    
    void Start(){
        rotationSpeed = Random.Range(0,5.5f);
    }

    void Update(){
       this.transform.Rotate(0,rotationSpeed,0);
    }
}
