using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.scripts
{
    public enum StatType {Health,MaxHealth,Mana,MaxMana,Speed,Armor,Haste,CastTime,Range,Ammo,MaxAmmo}

    public class Stat{
        private float _val;
        public StatType StatType;

        public LivingThing Wearer;

        public List<Func<float, float>> FlatStatModifiers = new List<Func<float, float>>();
        public List<Func<float, float>> PercentileStatModifiers = new List<Func<float, float>>();

        public Stat(float val){
            Set(val);
        }

        public float Get(){
            float valCopy = _val;
            //Wearer.BuffStatRequestSystem.GetFlatModifiers(StatType);
            //Wearer.BuffStatRequestSystem.GetPercentileModifiers(StatType);

            foreach (Func<float, float> modifier in FlatStatModifiers){
                valCopy += modifier(valCopy);
            }
            valCopy *= PercentileStatModifiers.Select(mod => mod(valCopy)).Sum() + 1;
            return valCopy;
        }

        public void Set(float val) {
            _val = val;
        }

        public static Stat operator- (Stat a, float b) {
            a._val -= b;
            return a;
        }

        public static Stat operator +(Stat a, float b) {
            a._val += b;
            return a;
        }

        public static implicit operator Stat(float v) {
            //CappedStat c = new CappedStat(1,1); causes stackoverflow
            return new Stat(v);
        }
    }

    public class CappedStat {
        private Stat _val;
        public Stat Cap;

        public CappedStat(float val, float cap) {
            _val = val;
            Cap = cap;
        }

        public float GetVal() {
            return _val.Get();
        }

        public void SetVal(float val) {
            _val = val;
        }

        public static CappedStat operator +(CappedStat a, float b) {
            a._val.Set(Math.Min(a._val.Get() + b, a.Cap.Get()));
            return a;
        }

        public static CappedStat operator -(CappedStat a, float b) {
            a._val.Set(Math.Min(a._val.Get() - b, a.Cap.Get()));
            return a;
        }

        public float PerCentage() {
            return _val.Get() / Cap.Get();
        }
    }
}

