using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.scripts {
    public class BuffStatRequestSystem {
        public LivingThing Wearer;

        public BuffStatRequestSystem(LivingThing wearer) {
            Wearer = wearer;
        }

        public float GetFlatModifiers(StatType statType) {
            foreach (KeyValuePair<Type, Buff> pair in Wearer.Buffs) {
                //pair.Value.getStatModifier(statType);
            }
            return 0;
        }

        public float GetPercentileModifiers(StatType statType) {
            throw new NotImplementedException();
        }
    }
}
