using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer
{
   //https://www.youtube.com/watch?v=1hsppNzx7_0&ab_channel=CodeMonkey
   
   public static FunctionTimer Create(Action action, float timer)
   {
      GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));

      
      FunctionTimer functionTimer = new FunctionTimer(action, timer, gameObject);
      gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;
      
      return functionTimer;
   }
   
   public class MonoBehaviourHook : MonoBehaviour
   {
      public Action onUpdate;

      private void Update()
      {
         if (onUpdate != null) onUpdate();
      }
   }
   
   private Action action;
   private float timer;
   private GameObject gameObject;
   private bool isDestroyed;
   
   private FunctionTimer(Action action, float timer, GameObject gameObject)
   {
      this.action = action;
      this.timer = timer;
      this.gameObject = gameObject;
      isDestroyed = false;
   }

   private void Update()
   {
      if(isDestroyed) return;
      
      timer -= Time.deltaTime;
      if (timer <= 0)
      {
         action();
         DestroySelf();
      }
   }

   private void DestroySelf()
   {
      isDestroyed = true;
      UnityEngine.Object.Destroy(gameObject);
   }
}


