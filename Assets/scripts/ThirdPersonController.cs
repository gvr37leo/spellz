using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts {
    class ThirdPersonController : MonoBehaviour {

        public Player target;
        public float mouseSensitivity = 500;
        public float scrollSensitivity = 10;
        float dstFromTarget = 5;
        float yaw;
        float pitch = 45;
        Vector3 offset;

        void Start() {
            offset = transform.position - target.transform.position;
        }

        void LateUpdate() {
            Vector2 mouseMovement = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * Time.deltaTime * mouseSensitivity;
            Vector3 straightForward = transform.forward;
            straightForward.y = 0;

            //if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1)) {

            //}
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)) {
                yaw += Input.GetAxis("Mouse X") * 10;
                pitch -= Input.GetAxis("Mouse Y") * 10;
            }
            if (Input.GetKey(KeyCode.Mouse1)) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                target.transform.forward = straightForward;
            } else {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1)) {
            }

            Debug.DrawLine(target.transform.position, target.transform.position + target.transform.forward * 10, Color.red);

            dstFromTarget -= Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;

            Vector3 targetRotatation = new Vector3(pitch, yaw);
            transform.eulerAngles = targetRotatation;
            transform.position = target.transform.position - transform.forward * dstFromTarget;
        }

    }
}
