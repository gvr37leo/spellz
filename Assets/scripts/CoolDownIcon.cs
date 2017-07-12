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
            Ability = Player.Abilities[Ability.GetType()];
            ShortcutText.text = Ability.ShortCutKey.ToString();

            Ability.OnCoolDownStart += () => {
                Mask.fillAmount = 1;
            };
            Ability.OnCoolDownTick += () => {
                Mask.fillAmount = 1 - (Ability.TimeSpentCoolingDown / Ability.Cooldown.Get());
            };
            Ability.OnCoolDownFinished += () => {
                Mask.fillAmount = 0;
            };
        };
    }
	
	// Update is called once per frame
	void Update () {
	    AmmoText.text = Ability.Ammo.GetVal().ToString();
	}
}
