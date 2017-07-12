using System;
using UnityEngine;

namespace Assets.scripts.buffs {

    [CreateAssetMenu(fileName = "Fervor", menuName = "Buffs/Fervor", order = 1)]
    public class Fervor : Buff {
        private Func<float, float> _castTimeModifier;

        protected override Buff Initialize(LivingThing wearer) {
            return this;
        }

        void Start () {
	
        }
	
        void Update () {
	    
        }

        public override void OnApply() {
            _castTimeModifier = castTime => -0.1f * Stacks.GetVal();
            Wearer.Abilities[typeof(Melee)].CastTime.FlatStatModifiers.Add(_castTimeModifier);
        }

        public override void OnRemove() {
            Wearer.Abilities[typeof(Melee)].CastTime.FlatStatModifiers.Remove(_castTimeModifier);
        }

        public override void Tick() {

        }
    }
}
