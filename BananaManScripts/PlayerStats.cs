using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerStats : MonoBehaviour{

   public ThirdPersonController thirdPersonController;
   public CharacterController characterController;
   public GameObject startingPosition;
   public GameValues gameValues;
   public Transform playerTransform;

   private int bananasCollected;
   
   private float slopeLimitIncrease;
   private float moveSpeedIncrease;
   private float sprintSpeedIncrease;
   private float startingSlopeLimit;
   private float startingMoveSpeed;
   private float startingSprintSpeed;

   void Awake(){
      ResetBananas();
      InitializeStats();
   }

   public void CollectBanana(int ammount){
      bananasCollected += ammount;
   }

   public void RespawnPlayer(){
      playerTransform.position = startingPosition.transform.position;
   }

   public int GetBenanasCollected(){
      return bananasCollected;
   }
   
   public void ResetBananas(){
      bananasCollected = 0;
   }

   public void UpgradeStats(){
      characterController.slopeLimit    += slopeLimitIncrease;
      thirdPersonController.MoveSpeed   += moveSpeedIncrease;
      thirdPersonController.SprintSpeed += sprintSpeedIncrease;
   }

   public void ResetStats(){
      characterController.slopeLimit    = startingSlopeLimit;
      thirdPersonController.MoveSpeed   = startingMoveSpeed;
      thirdPersonController.SprintSpeed = startingSprintSpeed;
   }

   private void InitializeStats(){

      startingSlopeLimit   = gameValues.startingSlopeLimit;
      startingMoveSpeed    = gameValues.startingMoveSpeed;
      startingSprintSpeed  = gameValues.startingSprintSpeed;

      slopeLimitIncrease  = gameValues.slopeLimitIncrease;
      moveSpeedIncrease   = gameValues.moveSpeedIncrease;
      sprintSpeedIncrease = gameValues.sprintSpeedIncrease;

      characterController.slopeLimit    = startingSlopeLimit;
      thirdPersonController.MoveSpeed   = startingMoveSpeed;
      thirdPersonController.SprintSpeed = startingSprintSpeed;

   }
}
