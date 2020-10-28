using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 15f;
    float vlimit = 10f;
    float PlaneBx = 5f;
    float PlaneBz = 20f;
    float gravitymod = 10f;
    float jumpcounter = 0f;
    bool plane = true;
    Rigidbody PlayerRb;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravitymod;
        
    }

    // Update is called once per frame
    void Update()
    {
        float VerticalInput = Input.GetAxis("Vertical");
        float HorizontalInput = Input.GetAxis("Horizontal");


        //movement of player
        transform.Translate(Vector3.right * Time.deltaTime * HorizontalInput *speed);
        transform.Translate(Vector3.forward * Time.deltaTime * VerticalInput *speed);
        jumpplayer();


        if (plane)
        {
            Debug.Log("Plane A");
            if (transform.position.z < -vlimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -vlimit);
            }
            if (transform.position.z > vlimit && transform.position.x > PlaneBx)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, vlimit);
            }
            if (transform.position.z > vlimit && transform.position.x < -PlaneBx)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, vlimit);
            }
            if (transform.position.x < -vlimit)
            {
                transform.position = new Vector3(-vlimit, transform.position.y, transform.position.z);
            }
            if (transform.position.x > vlimit)
            {
                transform.position = new Vector3(vlimit, transform.position.y, transform.position.z);
            }
        }
        else
        {
            Debug.Log("Plane B");
            if (transform.position.z > PlaneBz)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, PlaneBz);
            }
            if (transform.position.x < -PlaneBx)
            {
                transform.position = new Vector3(-PlaneBx, transform.position.y, transform.position.z);
            }
            if (transform.position.x > PlaneBx)
            {
                transform.position = new Vector3(PlaneBx, transform.position.y, transform.position.z);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane A"))
        {
            Debug.Log("Plane A");
            plane = true;
            jumpcounter = 0;
        }
        if (collision.gameObject.CompareTag("Plane B"))
        {
            Debug.Log("Plane B");
            plane = false;
            jumpcounter = 0;
        }
    }
    void jumpplayer()
    {
        if (jumpcounter == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerRb.AddForce(Vector3.up * 3 * gravitymod, ForceMode.Impulse);
                jumpcounter = 1;
            }
        }
    }
    
}
