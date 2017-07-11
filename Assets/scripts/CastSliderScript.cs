using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CastSliderScript : MonoBehaviour {
    private Slider _slider;
    public Player Player;

	void Start () {
	    _slider = GetComponent<Slider>();
	    Player.OnCastStart += ability => {
	        _slider.value = 0;
	    };
	    Player.OnCastTick += (f, ability) => {
	        _slider.value = f / ability.CastTime.Get();
	    };
	    Player.OnCastFinished += ability => {
	        _slider.value = 0;
        };
	}

	void Update () {
	    
	}
}
