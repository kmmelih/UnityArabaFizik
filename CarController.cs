using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float maxDonusAcisi;
    [SerializeField] private float motorTorku;
    [SerializeField] private float frenGucu;
    [SerializeField] private float arabaHizi;
    

    [SerializeField] private WheelCollider solOnTekerCollider;
    [SerializeField] private WheelCollider sagOnTekerCollider;
    [SerializeField] private WheelCollider solArkaTekerCollider;
    [SerializeField] private WheelCollider sagArkaTekerCollider;

    [SerializeField] private Transform solOnTekerTransform;
    [SerializeField] private Transform sagOnTekerTransform;
    [SerializeField] private Transform solArkaTekerTransform;
    [SerializeField] private Transform sagArkaTekerTransform;

    private float verticalInput;
    private float horizontalInput;
    private bool isFren;
    private float anlikFrenGucu; 
    private float anlikDonusAcisi;
    
    private void Update()
    {
        inputKontrol();
        arabaKontrol();
        donusKontrol();
        tekerDonder();
    }

    void inputKontrol()
    {
        verticalInput = Input.GetAxis("Vertical") * -1 * arabaHizi;
        horizontalInput = Input.GetAxis("Horizontal");
        isFren = Input.GetKey(KeyCode.Space);
    }

    void arabaKontrol()
    {
        solOnTekerCollider.motorTorque = verticalInput * motorTorku;
        sagOnTekerCollider.motorTorque = verticalInput * motorTorku;
        anlikFrenGucu = isFren ? frenGucu : 0f;
        if (isFren)
        {
            solArkaTekerCollider.brakeTorque = anlikFrenGucu;
            sagArkaTekerCollider.brakeTorque = anlikFrenGucu;
            solOnTekerCollider.brakeTorque = anlikFrenGucu;
            sagOnTekerCollider.brakeTorque = anlikFrenGucu;
        }
    }

    void donusKontrol()
    {
        anlikDonusAcisi = horizontalInput * maxDonusAcisi;
        sagOnTekerCollider.steerAngle = anlikDonusAcisi;
        solOnTekerCollider.steerAngle = anlikDonusAcisi;
    }

    void tekerDonder()
    {
        tekerDonder(sagOnTekerCollider,sagOnTekerTransform);
        tekerDonder(solOnTekerCollider,solOnTekerTransform);
        tekerDonder(sagArkaTekerCollider,sagArkaTekerTransform);
        tekerDonder(solArkaTekerCollider,solArkaTekerTransform);
    }

    void tekerDonder(WheelCollider tekerCollider, Transform tekerlekTransform) 
    {
        Vector3 position;
        Quaternion rotation;
        tekerCollider.GetWorldPose(out position, out rotation);
        tekerlekTransform.position = position;
        tekerlekTransform.rotation = rotation;
    }
}
