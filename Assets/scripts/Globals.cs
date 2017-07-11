using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Globals : MonoBehaviour {
    public static Globals Self;
    public static List<RuleCheck> DefaultSpellRuleCheck;
    public static RuleCheck MustHaveTarget;
    public static RuleCheck MustRespectGlobalCooldown;
    public static RuleCheck CantCastWhileCasting;

    void Awake() {
        if (Self == null) {
            Self = this;
        }
        MustHaveTarget = (spell, caster, target) => caster.Target != null;
        MustRespectGlobalCooldown = (spell, caster, target) => caster.GlobalCooldownRemaining <= 0;
        CantCastWhileCasting = (spell, caster, target) => caster.State != State.casting;

        DefaultSpellRuleCheck = new List<RuleCheck>{
            //(spell, caster, target) => spell.manaCost <= caster.mana,
            (spell, caster, target) => (target.transform.position - caster.transform.position).magnitude < spell.Range.Get(),
            //(spell, caster, target) => caster.velocity == Vector3.zero,
            (spell, caster, target) => spell.Ammo.Val.Get() > 0,
            (spell, caster, target) => spell.IsReady(),
            //MustRespectGlobalCooldown,
            CantCastWhileCasting
        };
    }

    void Update() {
        
    }

}
