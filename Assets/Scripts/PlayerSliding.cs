using UnityEngine;

public class PlayerSliding : MonoBehaviour
{
    [Header("Referencers")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMove pm;

    [Header("Sliding")]
    public float maxSlideTime = 0.75f;
    public float slideForce = 200f;
    private float slideTimer;

    public float slideYScale = 0.5f;
    private float startYScale;

    [Header("Input")]
    [SerializeField] private KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMove>();

        startYScale = playerObj.localScale.y;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0))
            StartSlide();

        if(Input.GetKeyUp(slideKey) && pm.sliding)
        {
            StopSlide();
        }
    }

    private void FixedUpdate()
    {
        if (pm.sliding)
            SlidingMovement();
    }

    public void StartSlide()
    {
        pm.sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //sliding normal
        if (!pm.OnSlope() || rb.velocity.y > -0.1f)
        {
            slideTimer -= Time.deltaTime;

            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
        }

        //sliding down a slope
        else
        {
            rb.AddForce(pm.GetSlopeMovementDirection(inputDirection) * slideForce, ForceMode.Force);

            //any difference with this?
            //if(rb.velocity.y > 0) rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        //stop sliding again
        if (slideTimer <= 0)
            StopSlide();
    }

    public void StopSlide()
    {
        pm.sliding = false;

        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }
}
