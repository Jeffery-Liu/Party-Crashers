using UnityEngine;
using System.Collections;
using System;


public class TranslateObject : MonoBehaviour {

    //Edit the rotation of the projectile.
    [Header("Rotation")]
    public bool rotate;
    public float rotationSpeed;
    public float offset;

    //Edit the translation of the projectile this is always constant and does not use force, it is only mathematical.
    [Header("Translate")]
    public bool forward;
    public float distanceSpeed;

    //Edit the force applied to a projectile using initial or constant force this can be effected in game.
    [Header("Force")]
    [Tooltip("Force requires a Rigidbody component.")]
    public bool initialForce;
    public Vector3 initialAppliedForce;
    public bool alwaysForce;
    public Vector3 alwaysAppliedForce;

    public enum forceType
    {
        Force,
        Acceleration,
        Impulse,
        VelocityChange
    };
    public forceType projectileForceType;

    //private string forceString;
    private Vector3 appliedVector;
    private Vector3 radiusPoint;


    void Start()
    {

        //forceString = "ForceMode" + projectileForceType.ToString();
        appliedVector = Vector3.forward * distanceSpeed;

        if (initialForce)
        {
            GetComponent<Rigidbody>().AddForce(initialAppliedForce, ForceMode.Force);
        }

    }

	void Update ()
    {
        applyTranslation();

    }

    void applyTranslation()
    {
        if (forward)
        {
            this.transform.Translate(appliedVector);
            Debug.Log("Forward");
        }

        if (rotate)
        {
            radiusPoint = Vector3.ClampMagnitude(radiusPoint, offset);
            this.transform.RotateAround(radiusPoint, Vector3.forward, rotationSpeed * Time.deltaTime);
            Debug.Log("Rotate.");
        }

        if (alwaysForce)
        {
            GetComponent<Rigidbody>().AddForce(alwaysAppliedForce, ForceMode.Force );
        }

    }
}
