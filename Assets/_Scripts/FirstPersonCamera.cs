using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {
    [SerializeField] private Transform eyesTransform;
    void LateUpdate() {
        transform.position = eyesTransform.position;
        transform.rotation = eyesTransform.rotation;
    }
}
