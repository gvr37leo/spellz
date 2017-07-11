using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoolDownIcon : MonoBehaviour {

	public Image Icon;
    public Image Mask;
    public Ability Ability;
    [HideInInspector]
    public Player Player;
    public Text AmmoText;
    public Text ShortcutText;

    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Player.OnInitFinished += () => {
            Ability ability = Player.Abilities[Ability.GetType()];

            ability.OnCoolDownStart += () => {
                Mask.fillAmount = 1;
            };
            ability.OnCoolDownTick += () => {
                Mask.fillAmount = 1 - (ability.TimeSpentCoolingDown / ability.Cooldown);
            };
            ability.OnCoolDownFinished += () => {
                Mask.fillAmount = 0;
            };
        };
    }
	
	// Update is called once per frame
	void Update () {
	    
	    
	}
}
