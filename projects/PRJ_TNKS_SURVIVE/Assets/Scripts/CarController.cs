using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{

    public float maxMotorTorque = 500; // maximum torque the motor can apply to wheel
    public float turnSpeed; // maximum steer angle the wheel can have
    //[0]Front 
    //Something
    public WheelCollider frontLeftWheel, frontRightWheel, backLeftWheel, backRightWheel;
    public SimplifiedTread treadLeft, treadRight;
    float torqueToAddL = 50;
    float torqueToAddR = -3;
    private float torq = 50;






    private void Start()
    {
        treadLeft = new SimplifiedTread(frontLeftWheel, backLeftWheel);
        treadRight = new SimplifiedTread(frontRightWheel, backRightWheel);
        Debug.Log(transform.forward);
        torq = maxMotorTorque;
            
    }
    public void FixedUpdate()
    {
        Debug.DrawRay(transform.position, -transform.forward, Color.blue, 2);

        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float theta = Vector3.Angle(transform.forward, moveVector);
        float firstDot = Vector3.Dot(Vector3.Cross(-transform.forward, moveVector), Vector3.up);
        float secondDot = Vector3.Dot(-transform.forward, moveVector);
        float signedTheta = Mathf.Atan2(firstDot, secondDot) * (180.0f / Mathf.PI);
        //Debug.Log("Theta: " + theta);
        Debug.Log("Signed Theta: " + signedTheta);
        if (moveVector != Vector3.zero)
        {
            if (Mathf.Abs(signedTheta) < 50)
            {
                float rescaledTheta = (1.0f / 180.0f) * (180 - Mathf.Abs(signedTheta));
                //float torq = rescaledTheta * 50;
                Debug.Log("Less than 0");
                //float rescaledTheta = (1.0f / 180.0f) * theta;
                float currentVelF = 0;
                float currentVelB = 0;
                treadLeft.Front.steerAngle = treadRight.Front.steerAngle = (1 / 180.0f) * signedTheta * 75f;
                treadLeft.Back.steerAngle = treadRight.Back.steerAngle = -(1 / 180.0f) * signedTheta * 75f;

                treadLeft.SetTorque(-torq);
                treadRight.SetTorque(-torq);
            }
            else if (Mathf.Abs(signedTheta) >= 50 && Mathf.Sign(signedTheta) > 0.0f)
            {
                float currentVelFL = 0;
                float currentVelBL = 0;
                float currentVelFR = 0;
                float currentVelBR = 0;

                treadLeft.Back.steerAngle = Mathf.SmoothDampAngle(treadLeft.Back.steerAngle, -45,
                    ref currentVelBL, 0.05f);
                treadLeft.Front.steerAngle = Mathf.SmoothDampAngle(treadLeft.Front.steerAngle, 45,
                    ref currentVelFL, 0.05f);
                treadRight.Back.steerAngle = Mathf.SmoothDampAngle(treadRight.Back.steerAngle, 45,
                   ref currentVelBR, 0.05f);
                treadRight.Front.steerAngle = Mathf.SmoothDampAngle(treadRight.Front.steerAngle, -45,
                    ref currentVelFR, 0.05f);


                //treadLeft.Back.steerAngle = treadRight.Back.steerAngle = -(1 / 180.0f) * signedTheta * 75f;

                treadLeft.SetTorque(-torq * 4);
                treadRight.SetTorque(torq * 4);

            }
            else if (Mathf.Abs(signedTheta) >= 50 && Mathf.Sign(signedTheta) < 0.0f)
            {
                float currentVelFL = 0;
                float currentVelBL = 0;
                float currentVelFR = 0;
                float currentVelBR = 0;

                treadLeft.Back.steerAngle = Mathf.SmoothDampAngle(treadLeft.Back.steerAngle, -45,
                    ref currentVelBL, 0.05f);
                treadLeft.Front.steerAngle = Mathf.SmoothDampAngle(treadLeft.Front.steerAngle, 45,
                    ref currentVelFL, 0.05f);
                treadRight.Back.steerAngle = Mathf.SmoothDampAngle(treadRight.Back.steerAngle, 45,
                   ref currentVelBR, 0.05f);
                treadRight.Front.steerAngle = Mathf.SmoothDampAngle(treadRight.Front.steerAngle, -45,
                    ref currentVelFR, 0.05f);


                //treadLeft.Back.steerAngle = treadRight.Back.steerAngle = -(1 / 180.0f) * signedTheta * 75f;

                treadLeft.SetTorque(torq * 4);
                treadRight.SetTorque(-torq * 4);

            }
            //Vector3 diff = moveVector.normalized + transform.forward.normalized;
            ////Move Tank forward if forward vector is equal to move vector
            //if (Mathf.Approximately(csl.stormutils.TruncateFloat(signedTheta, 1), 0.00f))
            //{
            //    Debug.Log("Moving tank straight");
            //    treadLeft.Front.steerAngle = 0;
            //    treadLeft.Back.steerAngle = 0;
            //    treadRight.Front.steerAngle = 0;
            //    treadRight.Back.steerAngle = 0;
            //    treadRight.SetTorque(-maxMotorTorque);
            //    treadLeft.SetTorque(-maxMotorTorque);


            //}
            //else if (signedTheta < 0.00f)
            //{
            //    float rescaledTheta = (1.0f / 180.0f) * (180 - Mathf.Abs(signedTheta));
            //    //float torq = rescaledTheta * 50;
            //    Debug.Log("Less than 0");
            //    //float rescaledTheta = (1.0f / 180.0f) * theta;
            //    float currentVelF = 0;
            //    float currentVelB = 0;
            //    treadLeft.Front.steerAngle = treadRight.Front.steerAngle = Mathf.SmoothDampAngle(
            //                                 treadLeft.Front.steerAngle,
            //                                 signedTheta, ref currentVelF, 0.2f);
            //    treadLeft.Back.steerAngle = treadRight.Back.steerAngle = Mathf.SmoothDampAngle(
            //                                  treadLeft.Front.steerAngle,
            //                                  signedTheta, ref currentVelB, 0.2f);

            //    treadLeft.SetTorque(-torq);
            //    treadRight.SetTorque(torq);
            //}
            //else if (signedTheta > 0.00f)
            //{
            //    float currentVelF = 0;
            //    float currentVelB = 0;
            //    treadLeft.Front.steerAngle =treadRight.Front.steerAngle= Mathf.SmoothDampAngle(
            //                               treadLeft.Front.steerAngle,
            //                               signedTheta, ref currentVelF, 0.2f);
            //    treadLeft.Back.steerAngle = treadRight.Back.steerAngle = Mathf.SmoothDampAngle(
            //                               treadLeft.Front.steerAngle,
            //                               signedTheta, ref currentVelB, 0.2f);
            //    treadLeft.SetTorque(torq);
            //    treadRight.SetTorque(torq);
            //}


        }
        else
        {
            treadLeft.SetTorque(0.0f);
            treadRight.SetTorque(0.0f);

            Debug.Log("stopping");
        }

    }
}

//axleInfo.leftWheel.motorTorque = motor;
//                axleInfo.rightWheel.motorTorque = motor;

public class SimplifiedTread
{
    public SimplifiedTread(WheelCollider front, WheelCollider back)
    {
        this.Front = front;
        this.Back = back;
    }

    public WheelCollider Front { get; set; }
    public WheelCollider Back { get; set; }

    public void SetTorque(float torque)
    {
        Front.motorTorque = Back.motorTorque = torque;
    }
    public void BreakTorque(float torque)
    {
        Front.motorTorque = Back.motorTorque = torque;
    }
    public float GetTorque()
    {
        return Front.motorTorque;
    }
}
