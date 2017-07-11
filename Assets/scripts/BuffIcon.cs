using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts {
    public class BuffIcon : MonoBehaviour {
        public Buff Buff;
        public Text DurationText;
        public Image Image;
        public RectTransform RectTransform;

        public BuffIcon Init(Buff buff) {
            Buff = buff;
            RectTransform = GetComponent<RectTransform>();
            buff.OnBuffTick += () => {
                DurationText.text = Buff.TimeLeft.ToString("F2");
            };
            return this;
        }

        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	    
        }
    }
}
