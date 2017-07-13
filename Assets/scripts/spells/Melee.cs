using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts;
using Assets.scripts.buffs;

//[CreateAssetMenu(fileName = "Melee", menuName = "Spells/Melee", order = 1)]
// ReSharper disable once CheckNamespace
public class Melee : Ability {
    public Fervor Fervor;
    public AudioClip SwordHitAudioClip;
    public AudioClip SwordMissAudioClip;

    protected override void Init() {
        CastTime.Set(0.8f);
        Cooldown.Set(0);
    }

    public override void Launch() {
        Hit(new List<LivingThing>{Caster.Target});
        Globals.Self.StartCoroutine(CoolingDown());
    }


    public override void Hit(List<LivingThing> enemys) {
        if (enemys.Count > 0) {
            AudioSource.PlayClipAtPoint(SwordHitAudioClip, Caster.transform.position);
        } else {
            AudioSource.PlayClipAtPoint(SwordMissAudioClip, Caster.transform.position);
        }

        foreach (LivingThing enemy in enemys) {
            enemy.TakeDamage(1, Caster);
        }
        Caster.ApplyBuff(Fervor.BaseInitialize(Caster), true);
    }

    
}
