using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    [Header("Velocidades")]
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    
    [Header("Giro")]
    [SerializeField] Vector3 normalSpin;
    [SerializeField] Vector3 handBreakSpin;

    [Header("Friccion y otros")]
    [SerializeField] float breakStrenth;
    [SerializeField] float handBreakStrenth;
    [SerializeField] float friction;
    bool HandBreak;

    [Header("Derrape")]
    [SerializeField] List<Transform> drifftSpawn;
    [SerializeField] GameObject drifftPF;

    [Header("Particulas")]
    [SerializeField] ParticleSystem forwardParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;
    Vector2 movement = Vector2.zero;

    void Start()
    {
       
        forwardParticles.Stop();
        leftParticles.Stop();
        rightParticles.Stop();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        MovementInput();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void MovementInput()
    {
        movement = Vector2.zero;

        ////Comportamientos relacionados con la tecla W////
        if (Input.GetKey(KeyCode.W))
        {
            movement.x += 1;
        }

        ////Frenado y MarchaAtras////
        if (Input.GetKey(KeyCode.S))
        {
            movement.x -= 1;
        }

        ////Giro////

        if (Input.GetKey(KeyCode.D))
        {
            movement.y -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement.y += 1;
        }

        ////Frenado////
        HandBreak = Input.GetKey(KeyCode.Space);
        
        ////Particulas////
        if (Input.GetKeyDown(KeyCode.W))
            forwardParticles.Play();

        if (Input.GetKeyDown(KeyCode.D))
            leftParticles.Play();

        if (Input.GetKeyDown(KeyCode.A))
            rightParticles.Play();

        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            leftParticles.Stop();
            rightParticles.Stop();
        }
    }


    void Movement()
    {
        
        if(movement.x < 0)
        {

            if(transform.InverseTransformDirection(rb.velocity).z < 0)
            {
                rb.velocity -= speed * Time.fixedDeltaTime * transform.forward;
            }

            else
            {
                rb.velocity = new Vector3(
                        rb.velocity.x / (1 + breakStrenth * Time.fixedDeltaTime),
                        rb.velocity.y,
                        rb.velocity.z / (1 + breakStrenth * Time.fixedDeltaTime));
            }
        }

        if (HandBreak)
        {
            rb.velocity = new Vector3(
                        rb.velocity.x / (1 + handBreakStrenth * Time.fixedDeltaTime),
                        rb.velocity.y,
                        rb.velocity.z / (1 + handBreakStrenth * Time.fixedDeltaTime));
            
            for (int i = 0; i < drifftSpawn.Count; i++)
            {
                GameObject drifftInstance = Instantiate(drifftPF,
                    drifftSpawn[i].position,
                    Quaternion.identity);
                drifftPF.transform.LookAt(drifftSpawn[i].position);
                Destroy(drifftInstance, 0.5f);
            }
        }
        

        if(movement.y < 0)
        {
            rb.angularVelocity -= new Vector3(
                rb.angularVelocity.x ,
                rb.angularVelocity.y - normalSpin.y);

            if (HandBreak)
            {
                rb.angularVelocity -= new Vector3(
                rb.angularVelocity.x,
                rb.angularVelocity.y - handBreakSpin.y);
            }
        }

        if(movement.y > 0)
        {
            rb.angularVelocity -= new Vector3(
                rb.angularVelocity.x,
                rb.angularVelocity.y + normalSpin.y);

            if (HandBreak)
            {
                rb.angularVelocity -= new Vector3(
                rb.angularVelocity.x,
                rb.angularVelocity.y + handBreakSpin.y);
            }
        }

        if (movement.x > 0)
        {
            rb.velocity += speed * Time.fixedDeltaTime * transform.forward;

        }

        else
        {
            rb.velocity = new Vector3(
                rb.velocity.x / (1 + friction * Time.deltaTime),
                rb.velocity.y,
                (rb.velocity.z / (1 + friction * Time.deltaTime)));
            //print(friccion);
            forwardParticles.Stop();
            
        }

        if (rb.velocity.x >= maxSpeed || rb.velocity.z >= maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
         
        }
        
    }

}
