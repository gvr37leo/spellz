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

    
}
