using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.scripts
{
    public class Stat{
    private float _val;

    public List<Func<float, float>> FlatStatModifiers = new List<Func<float, float>>();
    public List<Func<float, float>> PercentileStatModifiers = new List<Func<float, float>>();

    public Stat(float val){
        Set(val);
    }

    public float Get(){
        float valCopy = _val;
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

    public static implicit operator Stat(float v) {
        return new Stat(v);
    }
}

public class CappedStat
{
    public Stat Val;
    public Stat Cap;

    public CappedStat(float val, float cap) {
        Val = val;
        Cap = cap;
    }

    public void Set(float val) {
        Val.Set(val);
    }
}
}

