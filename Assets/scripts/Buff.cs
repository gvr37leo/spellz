using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts
{
    public abstract class Buff : ScriptableObject {
        public event Action OnBuffStart;
        public event Action OnBuffTick;
        public event Action OnBuffFinished;

        public int Stacks = 1;
        public float TimeLeft = 3;
        public float Duration = 3;
        [HideInInspector]
        public LivingThing Wearer;
        public LivingThing Caster;
        public Sprite Image;
        public abstract void OnApply();
        public abstract void OnRemove();

        public Buff BaseInitialize(LivingThing wearer) {
            Buff buff = Instantiate(this);
            buff.Wearer = wearer;
            buff.TimeLeft = Duration;
            OnBuffTick += Tick;
            return buff.Initialize(wearer);
        }

        protected abstract Buff Initialize(LivingThing wearer);

        public IEnumerator Progress() {
            if (OnBuffStart != null) OnBuffStart();
            while (TimeLeft > 0) {
                TimeLeft -= Time.deltaTime;
                if (OnBuffTick != null) OnBuffTick();
                yield return null;
            }
            Wearer.RemoveBuff(this);
            if (OnBuffFinished != null) OnBuffFinished();
        }

        public abstract void Tick();
    }
}
