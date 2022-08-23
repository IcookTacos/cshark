using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class FlameMode : MonoBehaviour
{

    public ParticleSystem flame;
    public ThirdPersonController thirdPersonController;

    private float flameThreshold = 15;

    void Update(){
        var em = flame.emission;
        if(thirdPersonController._speed > flameThreshold){
            em.enabled = true;
        } else {
            em.enabled = false;
        }
        
    }
}
