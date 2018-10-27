using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController> {

    public float walkSpeed = 2f;
    public float runSpeed = 8f;
    public float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.1f;
    public AnimationClip clip;
    public KeyCode InteractKeyCode = KeyCode.E;
    
    private float turnSmoothVelocity;
    private float speedSmoothVelocity;
    private float currentSpeed;
    private Animator playerAnimator;
    private float walkPercentage = 0.5f;
    private CharacterController controller;
    private bool canDetectInput = true;
    private bool isPlayingPickupAnimation = false;

    public Vector3 velocity;

    public bool IsPlayingPickupAnimation
    {
        get { return isPlayingPickupAnimation; }
    }

    // Use this for initialization
    void Start () {
        playerAnimator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        Debug.Log("No hay arrastrado ningún target");
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
        PlayerControl();
    }

    void PlayerControl()
    {
        if (canDetectInput)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = input.normalized;
            
            if (inputDir != Vector2.zero)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            }

            bool running = Input.GetKey(KeyCode.LeftShift);

            float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
            float animationSpeedPercentage = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * walkPercentage);
            playerAnimator.SetFloat("speedPercentage", animationSpeedPercentage, speedSmoothTime, Time.deltaTime);

            velocity = transform.forward * currentSpeed;

            controller.Move(velocity * Time.deltaTime);

            currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;
        }    
    }

    void PickObject(StolableUI item)
    {
        if (!isPlayingPickupAnimation)
        {
            if (Input.GetKeyDown(InteractKeyCode))
            {
                isPlayingPickupAnimation = true;
                StartCoroutine(PickUpObjectSequence(item));
                
            }
            
        }
    }

    private IEnumerator PickUpObjectSequence(StolableUI item)
    {
        playerAnimator.SetTrigger("Pick");
        controller.enabled = false;
        canDetectInput = false;
        item.OnInteractKeyPressed(InteractKeyCode);

        while (Input.GetKey(InteractKeyCode))
        {
            isPlayingPickupAnimation = false;
            controller.enabled = true;
            canDetectInput = true;
            yield return null;
        }

        playerAnimator.SetTrigger("InterruptPick");
        isPlayingPickupAnimation = false;
        controller.enabled = true;
        canDetectInput = true;    
    }

    private void OnTriggerStay(Collider other)
    {
        StolableUI item = other.GetComponent<StolableUI>();

        if (item != null)
        {
            PickObject(item);
        }
        
    }
}
