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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement.x += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.angularVelocity += rotate;
            leftParticles.Play();
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.angularVelocity -= rotate;
            rightParticles.Play();
        }

        else
        {
            forwardParticles.Stop();
            leftParticles.Stop();
            rightParticles.Stop();
        }

        if (Input.GetKey(KeyCode.W))
        {
            forwardParticles.Play();
        }

        //friction
        Vector3 friction = -rb.velocity.normalized * stats.groundFriction;
        rb.velocity += friction * Time.deltaTime;


        currentSpeed = rb.velocity.magnitude;

        //velocidad maxima
        if (currentSpeed > stats.maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * stats.maxSpeed;
            currentSpeed = stats.maxSpeed;
        }

        //aceleration
        rb.velocity += movement * stats.forwardAcceleration * Time.deltaTime;
    }
}