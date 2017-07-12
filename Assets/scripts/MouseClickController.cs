using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MouseClickController : MonoBehaviour {

    Player player;
    private List<LivingThing> enemys;
    public LayerMask layerMask;

    void Start() {
        player = GetComponent<Player>();
        enemys = GameObject.FindGameObjectsWithTag("enemy").Select(obj => obj.GetComponent<LivingThing>()).ToList();
    }

    void Update() {
        

        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1)) {
            float closestDistance = Vector3.Distance(Input.mousePosition, Camera.main.WorldToScreenPoint(enemys[0].gameObject.transform.position));
            LivingThing closest = enemys[0];

            for (int i = 1; i < enemys.Count; i++) {
                float dist = Vector3.Distance(Input.mousePosition, Camera.main.WorldToScreenPoint(enemys[i].gameObject.transform.position));
                if (dist < closestDistance) {
                    closestDistance = dist;
                    closest = enemys[i];
                }
            }
            player.TargetSomething(closest);
        }
    }

    public void OnDrawGizmos() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 100);
    }
}
