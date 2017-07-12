using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : LivingThing {
    [HideInInspector]
    public List<Ability> AbilityStack = new List<Ability>();
    

    public override void Init () {
        
    }


    void Update () {
        Vector3 keyBoardInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 speed = transform.TransformDirection(keyBoardInput * Time.deltaTime * Speed.Get());
        gameObject.transform.position += speed;
        

        foreach (KeyValuePair<Type,Ability> pair in Abilities){
	        if (Input.GetKeyDown(pair.Value.ShortCutKey)){
	            AbilityStack.Insert(0, pair.Value);
	            if (AbilityStack.Count > 1) AbilityStack.RemoveAt(1);
	        }
	    }
	    if (AbilityStack.Count > 0){
	        Ability spell = AbilityStack[0];
	        if (spell.CompliesToRules()){
	            StartCoroutine(Cast(AbilityStack[0]));
	            
	            AbilityStack.RemoveAt(0);
	        }
	    }
    }

    public void TargetSomething(LivingThing newTarget) {
        if (Target != null) Target.gameObject.GetComponent<Renderer>().material.color = Color.red;
        Target = newTarget;
        Target.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
}
