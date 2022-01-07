using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worshiper : MonoBehaviour
{
    int stance;
    Animator animator;
    void Start(){
        stance = 0;
        animator = GetComponent<Animator>();
        setStance();
    }

    public void setStance(){
        stance = Random.Range(0,3);
        if(stance == 0) animator.SetTrigger("Praying0");
        if(stance == 1) animator.SetTrigger("Praying1");
        if(stance == 2) animator.SetTrigger("Praying2");
    }

}
