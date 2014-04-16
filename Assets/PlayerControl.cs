using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public Camera playerCamera;
    private const float MAX_SPEED = 5;
    private const float MAX_SPEED_DELTA = 1.5f;

    private bool _attacking = false;
    public bool _thrust = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Jump();
        }
	}

    void FixedUpdate()
    {



        UpdateMovement();

        GetComponent<Animator>().SetFloat("Speed", rigidbody.velocity.magnitude);
        
        var direction = rigidbody.velocity;
        direction.y = 0;
        if (direction.magnitude > 0.1)
            rigidbody.rotation = Quaternion.LookRotation(direction);

        playerCamera.transform.position = transform.position + Vector3.up * 5 + Vector3.back * 6;
    }

    private void UpdateMovement()
    {
        float maxSpeedChange = MAX_SPEED_DELTA;

        Vector3 dir = Vector3.zero;


        if (transform.position.y > 1)
            maxSpeedChange = MAX_SPEED_DELTA / 20f;
        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack") || _thrust)
        {
            if (Input.GetKey(KeyCode.W))
                dir += Vector3.forward;
            if (Input.GetKey(KeyCode.S))
                dir += Vector3.back;
            if (Input.GetKey(KeyCode.A))
                dir += Vector3.left;
            if (Input.GetKey(KeyCode.D))
                dir += Vector3.right;
        }
        else
            maxSpeedChange = MAX_SPEED_DELTA / 3.0f;

        dir.Normalize();

        var diff = dir * MAX_SPEED - rigidbody.velocity;
        var vel = rigidbody.velocity;
        diff.y = 0;
        vel.y = 0;


        if (diff.magnitude > maxSpeedChange)
        {
            diff.Normalize();
            diff *= maxSpeedChange;
        }

        rigidbody.AddForce(diff, ForceMode.VelocityChange);
    }

    private void Jump()
    {
        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            GetComponent<Animator>().SetTrigger("Jump");
            rigidbody.AddForce(Vector3.up * 4, ForceMode.VelocityChange);
            Debug.Log("JUUUUUMP");
        }
    }
}
