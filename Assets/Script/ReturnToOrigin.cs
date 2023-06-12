using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReturnToOrigin : MonoBehaviour
{
    [SerializeField] private Pose originPose;
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        originPose.position = transform.position;
        originPose.rotation = transform.rotation;
    }

    private void OnEnable() => grabInteractable.selectExited.AddListener(GunReleased);

    private void OnDisable() => grabInteractable.selectExited.RemoveListener(GunReleased);

    private void GunReleased(SelectExitEventArgs arg0)
    {
        transform.position = originPose.position;
        transform.rotation = originPose.rotation;
    }
}
