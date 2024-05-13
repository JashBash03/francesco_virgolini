using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStats stats;

    Rigidbody rb;
    Vector3 rotate = new Vector3 (0, 0.1f, 0);
    float currentSpeed = 0f;

    [SerializeField] ParticleSystem forwardParticles;
    [SerializeField] ParticleSystem rightParticles;
    [SerializeField] ParticleSystem leftParticles;
    
    Vector3 movement = Vector3.zero;

    bool isForwardPlaying;
    bool isRightPlaying;
    bool isLeftPlaying;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        forwardParticles.Stop();
        rightParticles.Stop();
        leftParticles.Stop();
    }

    void Update()
    {
        MovementInput();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if(movement.x > 0)
        {
            rb.velocity += transform.forward * stats.forwardAcceleration * Time.deltaTime;
        }
        else
        {
            //friction
            rb.velocity = new Vector3(
                    rb.velocity.x / (1 + stats.groundFriction * Time.deltaTime),
                    rb.velocity.y,
                    (rb.velocity.z / (1 + stats.groundFriction * Time.deltaTime)));
            print("Toy dentro");
            // Vector3 friction = -rb.velocity.normalized * stats.groundFriction;
            // rb.velocity += friction * Time.deltaTime;
        }

        currentSpeed = rb.velocity.magnitude;

        //velocidad maxima
        if (currentSpeed >= stats.maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * stats.maxSpeed;
            currentSpeed = stats.maxSpeed;
        }
    }

    void MovementInput()
    {
        movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movement.x += 1;
            if (!isForwardPlaying)
            {
                forwardParticles.Play();
                isForwardPlaying = true;
            }
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            if (isForwardPlaying)
            {
                forwardParticles.Stop();
                isForwardPlaying = false;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.angularVelocity += rotate;
            if (!isRightPlaying)
            {
                rightParticles.Play();
                isRightPlaying = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            if (isRightPlaying)
            {
                rightParticles.Stop();
                isRightPlaying = false;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.angularVelocity -= rotate;
            if (!isLeftPlaying)
            {
                leftParticles.Play();
                isLeftPlaying = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            if (isLeftPlaying)
            {
                leftParticles.Stop();
                isLeftPlaying = false;
            }
        }

        CheckParticles();
    }

    void CheckParticles()
    {
        isForwardPlaying = forwardParticles.isPlaying;
        isRightPlaying = rightParticles.isPlaying;
        isLeftPlaying = leftParticles.isPlaying;
    }
}