using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using Assets.scripts;

public enum State { idle, casting, channeling }

public abstract class LivingThing : MonoBehaviour {
    public Vector3 Facing = Vector3.forward;
    public BuffStatRequestSystem BuffStatRequestSystem;
    public event Action DeathEvent;
    public event Action<float, LivingThing> TakeDamageEvent;
    public event Action<Ability> OnCastStart;
    public event Action<float, Ability> OnCastTick;
    public event Action<Ability> OnCastFinished;
    public event Action<Buff> OnBuffAdded;
    public event Action<Buff> OnBuffRemoved;
    public event Action OnInitFinished;

    //stats
    public CappedStat Health = new CappedStat(100, 100);
    public CappedStat Mana = new CappedStat(100, 100);
    public Stat Speed = new Stat(3);
    public Stat Armor = new Stat(5);
    public Stat Haste = new Stat(1);

    public LivingThing Target;
    public Dictionary<Type, Buff> Buffs = new Dictionary<Type, Buff>();
    public List<Ability> AbilityAssignmentList = new List<Ability>();
    public Dictionary<Type, Ability> Abilities = new Dictionary<Type, Ability>();
    public float GlobalCooldown = 1;
    public float GlobalCooldownRemaining = 1;
    public State State;

    public void Start() {
        AbilityAssignmentList.RemoveAll(item => item == null);
        foreach (Ability ability in AbilityAssignmentList) {
            Ability temp = Instantiate(ability);
            temp.BaseInit(this);
            Abilities.Add(ability.GetType(), temp);
        }
        Init();
        if (OnInitFinished != null) OnInitFinished();
    }

    public abstract void Init();

    void Update() {

    }

    public IEnumerator Cast(Ability ability) {
        float timeSpentCastig = 0;
        if (OnCastStart != null) OnCastStart(ability);
        State = State.casting;
        yield return null;

        while (timeSpentCastig < ability.CastTime.Get()) {
            timeSpentCastig += Time.deltaTime;
            if (OnCastTick != null) OnCastTick(timeSpentCastig, ability);
            yield return null;
        }

        State = State.idle;
        ability.Launch();
        if (OnCastFinished != null) OnCastFinished(ability);
    }

    public void TakeDamage(float damage, LivingThing attacker) {
        Health -= damage;
        if (TakeDamageEvent != null) TakeDamageEvent(damage, attacker);
        if (Health.GetVal() <= 0) {
            Die();
        }
    }

    public void Die() {
        if (DeathEvent != null) DeathEvent();
        Destroy(gameObject);
    }

    public void ApplyBuff(Buff buff, bool increaseStacks = false) {
        if (Buffs.ContainsKey(buff.GetType())) {
            Buffs[buff.GetType()].Reset();
            if (increaseStacks) {
                Buffs[buff.GetType()].Stacks += 1;
            }
        }else {
            Buffs.Add(buff.GetType(), buff);
            Globals.Self.StartCoroutine(buff.Progress());
            if (OnBuffAdded != null) OnBuffAdded(buff);
        }
    }

    public void RemoveBuff(Buff buff) {
        if (Buffs.ContainsKey(buff.GetType())) {
            Buffs.Remove(buff.GetType());
            if (OnBuffRemoved != null) OnBuffRemoved(buff);
        }
    }

    public void DecreaseBuffStacks(Buff buff) {
        if (Buffs.ContainsKey(buff.GetType()) && Buffs[buff.GetType()].Stacks.GetVal() > 1) {
            Buffs[buff.GetType()].Stacks -= 1;
        }else {
            RemoveBuff(buff);
        }
    }
}
