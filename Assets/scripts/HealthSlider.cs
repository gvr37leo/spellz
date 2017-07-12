using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour {
    public LivingThing LivingThing;
    private Slider _slider;
	
	void Start () {
	    _slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
	    _slider.value = LivingThing.Health.PerCentage();
	}
}
