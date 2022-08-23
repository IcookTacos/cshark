using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour{
   
   void OnCollisionEnter(Collision c){
        Debug.Log("Collision");
        Debug.Log(c);
   }

   void OnTriggerEnter(Collider c){
    Debug.Log("Trigger");
    Debug.Log(c);
   }
}
