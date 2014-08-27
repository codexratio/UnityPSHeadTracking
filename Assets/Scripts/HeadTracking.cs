using UnityEngine;
using System;
using System.Collections;

public class HeadTracking : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public Vector3 offset = Vector3.zero;

    public Vector3 correction = Vector3.one;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }


    void Update()
    {

        uint playerID = KinectManager.Instance != null ? KinectManager.Instance.GetPlayer1ID() : 0;

        if (playerID <= 0)
        {
            if (transform.position != initialPosition)
                transform.position = initialPosition;

            if (transform.rotation != initialRotation)
                transform.rotation = initialRotation;

            return;
        }

        // set the position in space
        Vector3 posCamera = KinectManager.Instance.GetUserPosition(playerID);

        if (KinectManager.Instance.IsJointPositionTracked(playerID, 0))
        {
            Vector3 posJoint = KinectManager.Instance.GetJointPosition(playerID, 0);
            Quaternion rotJoint = KinectManager.Instance.GetJointOrientation(playerID, 0, false);

            posJoint -= posCamera;
            posJoint.z = -posJoint.z;


            transform.localPosition = posJoint  + offset;
            transform.localPosition = new Vector3(transform.localPosition.x * correction.x,
                transform.localPosition.y * correction.y,
                transform.localPosition.z * correction.z);    
            //transform.localRotation = rotJoint;
        }

    }

}
