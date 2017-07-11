using UnityEngine;
using System.Collections;
using Assets.scripts;
using Assets.scripts.buffs;

[CreateAssetMenu(fileName = "Melee", menuName = "Spells/Melee", order = 1)]
// ReSharper disable once CheckNamespace
public class Melee : Ability {
    public Fervor Fervor;

    protected override void Init() {
        
    }

    public override void Launch() {
        Hit();
        Globals.Self.StartCoroutine(CoolingDown());
    }


    public override void Hit() {
        Caster.Target.TakeDamage(10, Caster);
        Caster.ApplyBuff(Fervor.BaseInitialize(Caster));
    }

    
}
