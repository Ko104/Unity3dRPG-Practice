using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public float lookSmooth = 0.1f;
    public Vector3 offsetFromTarget = new Vector3(0, 6, -8);
    public float xTilt = 10;

    Vector3 destination = Vector3.zero;
    CharacterController charController;
    float rotateVel = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetCameraTarget(target);
    }

    public void SetCameraTarget(Transform t)   // can point to any Transform, call from external objects etc
    {
        target = t;
        if(t != null)
        {
            if (target.GetComponent<CharacterController>())
            {
                charController = target.GetComponent<CharacterController>();
            }
            else
                Debug.Log("Camera Target needs Character Controller");
        }
        else
            Debug.Log("No Camera Target");
    }

    void LateUpdate()   // Called Post Update calls ensures movements and rotations are based on current character movements
    {
        MoveToTarget();
        LookAtTarget();
    }

    void MoveToTarget()
    {
        destination = charController.TargetRotation * offsetFromTarget;
        destination += target.position;
        transform.position = destination;
    }

    void LookAtTarget()
    {
        float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerYAngle, 0);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
