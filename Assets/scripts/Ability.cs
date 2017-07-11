using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts;

public delegate bool RuleCheck(Ability spell, LivingThing caster, LivingThing target);

public abstract class Ability : ScriptableObject {

    public event Action OnCoolDownStart;
    public event Action OnCoolDownTick;
    public event Action OnCoolDownFinished;

    public KeyCode ShortCutKey;
    public LivingThing Caster;
    public Stat CastTime = 1;
    public Stat Range = 10;
    public float Cooldown = 2;
    public float TimeSpentCoolingDown = 0;
    public CappedStat Ammo = new CappedStat(1,1);
    
    public List<RuleCheck> Rules = new List<RuleCheck>();

    public void BaseInit(LivingThing caster) {
        Rules = Globals.DefaultSpellRuleCheck;
        Caster = caster;
        Init();
    }

    protected abstract void Init();

    void Start() {
        
    }

    void Update() {
        
    }

    public abstract void Launch();

    public abstract void Hit();

    public IEnumerator CoolingDown(){
        TimeSpentCoolingDown = 0;
        if (OnCoolDownStart != null) OnCoolDownStart();
        yield return null;

        while (TimeSpentCoolingDown < Cooldown){
            TimeSpentCoolingDown += Time.deltaTime;
            if (OnCoolDownTick != null) OnCoolDownTick();
            yield return null;
        }

        if (OnCoolDownFinished != null) OnCoolDownFinished();
    }

    public bool IsReady() {
        return TimeSpentCoolingDown >= Cooldown;
    }

    public bool CompliesToRules(){
        foreach (RuleCheck r in Rules)
            if (r(this, Caster, Caster.Target) == false)
                return false;
        return true;
    }
}
