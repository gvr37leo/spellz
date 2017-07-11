using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts;

public class BuffUiHandler : MonoBehaviour {
    [HideInInspector]
    public Player Player;
    public BuffIcon Prefab;
    [HideInInspector]
    public List<BuffIcon> BuffIcons = new List<BuffIcon>();
    public Dictionary<Buff, BuffIcon> BuffPairs = new Dictionary<Buff, BuffIcon>();
    public RectTransform BuffHolder;

    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Player.OnBuffAdded += buff => {
            BuffIcon buffIcon = Instantiate(Prefab).Init(buff);
            buffIcon.gameObject.transform.parent = BuffHolder.gameObject.transform;
            BuffPairs.Add(buff, buffIcon);
            BuffIcons.Add(buffIcon);
            ReAllignBuffIcons();
        };
        Player.OnBuffRemoved += buff => {
            BuffIcon buffIcon = BuffPairs[buff];
            BuffIcons.Remove(buffIcon);
            BuffPairs.Remove(buff);
            Destroy(buffIcon.gameObject);
            ReAllignBuffIcons();
        };
    }

    void ReAllignBuffIcons() {
        float x = 25;
        for (int i = 0; i < BuffIcons.Count; i++) {
            BuffIcons[i].RectTransform.localPosition = new Vector3(x,-25,0);
            x += 50;
        }
    }

	void Update () {
	    
	}
}
