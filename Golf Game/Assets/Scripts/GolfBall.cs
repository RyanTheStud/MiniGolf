 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GolfBall : MonoBehaviour
{
    public bool isIdle;

    public bool isAiming;

    public bool isShooting;

    public Rigidbody rb;

    public Camera MainCamera;

    public float stopVelocity;

    public float shotPower;

    public float maxPower;

    public int strokes;

    public GameObject MenuCanvas;

    Vector3? worldPoint;

    Vector3? point1;

    Vector3? point2;

    LayerMask layerMask;

    void Awake()
    {
        MainCamera = Camera.main;
        isAiming = false;
        rb.maxAngularVelocity = maxPower;
    }



    void FixedUpdate()
    {
        if (rb.velocity.magnitude < stopVelocity)
        {
            stop();
        }
        else isIdle = false;

        if (isShooting)
        {
            Shoot(worldPoint.Value);
            isShooting = false;
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            stopVelocity = 0.01f;
        }
    }

    public void stop()
    {       
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;        

        isIdle = true;
    }

    private Vector3? castRay()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit)) return hit.point;
        else return null;
    }

    private void Shoot(Vector3 point)
    {
        isAiming = false;

        Vector3 horizontalWorldPoint = new Vector3(point.x, transform.position.y, point.z);

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;

        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);
        rb.AddForce(-direction * strength * shotPower);

        strokes++;
    }


    private void processAim()
    {
        if (!isAiming && !isIdle) return;

        worldPoint = castRay();
    }

    void Update()
    {
        if (rb.velocity.magnitude < stopVelocity && Time.timeScale == 1)
        {
            processAim();

            if (Input.GetMouseButtonDown(0))
            {
                if (isIdle) isAiming = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isShooting = true;
            }            
        }
    }

    //private void Shoot(Vector3 point1, Vector3 point2)
    //{
    //    isAiming = false;

    //    Vector3 point1Pos = new Vector3(point1.x, point1.y, point1.z);
    //    Vector3 point2Pos = new Vector3(point2.x, point2.y, point2.z);
    //    Vector3 direction = (point1Pos - point2Pos).normalized;

    //    float strength = Vector3.Distance(point1, point2);
    //    rb.AddForce(direction * strength * shotPower);
    //}

        //private void processAim()
    //{
    //    if (!isAiming && !isIdle) return;

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        point1 = castRay();
    //    }
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        point2 = castRay();
    //    }

    //    if (!worldPoint.HasValue) return;
    //}

    
    //void Update()
    //{
    //    if (rb.velocity.magnitude < stopVelocity)
    //    {
    //        processAim();

    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            if (isIdle) isAiming = true;
    //        }

    //        if (Input.GetMouseButtonUp(0))
    //        {
    //            isShooting = true;
    //        }
    //    }
    //}
}
