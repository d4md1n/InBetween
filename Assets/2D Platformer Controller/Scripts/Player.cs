using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f; 
    public float timeToJumpApex = .4f;
    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    private float moveSpeed = 6f;

  //  public Vector2 wallJumpClimb;
  //  public Vector2 wallJumpOff;
  //  public Vector2 wallLeap;

    public bool canDoubleJump;
    private bool isDoubleJumping = false;

   // public float wallSlideSpeedMax = 3f;
 //   public float wallStickTime = .25f;
  //  private float timeToWallUnstick;

    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;

    private Controller2D controller;
    Animator moving;
    Animator stoping;
    private Vector2 directionalInput;
  //  private bool wallSliding;
  //  private int wallDirX;
    public static bool killed1;
    public static bool killed2;
    private void Start()
    {
        killed1 = false;
        killed2 = false;
        moving = GetComponent<Animator>();
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }
   
    private void Update()
    {
        Vector3 vel = CalculateVelocity();
     //   HandleWallSliding();

        controller.Move(velocity * Time.deltaTime, directionalInput);
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moving.SetTrigger("Move");
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moving.SetTrigger("Stop");
        }
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    public void SetDirectionalInput(Vector2 input)
    {
        
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
     //   moving.SetTrigger("Move");
        //if (wallSliding)
        //{
        //    if (wallDirX == directionalInput.x)
        //    {
        //        velocity.x = -wallDirX * wallJumpClimb.x;
        //        velocity.y = wallJumpClimb.y;
        //    }
        //    else if (directionalInput.x == 0)
        //    {
        //        velocity.x = -wallDirX * wallJumpOff.x;
        //        velocity.y = wallJumpOff.y;
        //    }
        //    else
        //    {
        //        velocity.x = -wallDirX * wallLeap.x;
        //        velocity.y = wallLeap.y;
        //    }
        //    isDoubleJumping = false;
     //   }
        if (controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = false;
        }
        if (canDoubleJump && !controller.collisions.below && !isDoubleJumping )//&& !wallSliding)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = false;
        }
    }

    public void OnJumpInputUp()
    {
      //  moving.SetTrigger("Stop");
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    //private void HandleWallSliding()
    //{
    //    wallDirX = (controller.collisions.left) ? -1 : 1;
    //    wallSliding = false;
    //    if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
    //    {
    //        wallSliding = true;

    //        if (velocity.y < -wallSlideSpeedMax)
    //        {
    //            velocity.y = -wallSlideSpeedMax;
    //        }

    //        if (timeToWallUnstick > 0f)
    //        {
    //            velocityXSmoothing = 0f;
    //            velocity.x = 0f;
    //            if (directionalInput.x != wallDirX && directionalInput.x != 0f)
    //            {
    //                timeToWallUnstick -= Time.deltaTime;
    //            }
    //            else
    //            {
    //                timeToWallUnstick = wallStickTime;
    //            }
    //        }
    //        else
    //        {
    //            timeToWallUnstick = wallStickTime;
    //        }
    //    }
    //}

    private Vector3 CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne));
        velocity.y += gravity * Time.deltaTime;
        return velocity;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (gameObject.tag == "main" && (coll.gameObject.tag == "Kill1" || coll.gameObject.tag == "Kill2"))
        {
            Destroy(coll.gameObject);
            possess.level++;
        }
        if (gameObject.tag == "Player")
        {
            if (coll.gameObject.tag == "Kill1")
            {

                Destroy(coll.gameObject);
                killed1 = true;
            //    possess.level++;

            }
            if (coll.gameObject.tag == "Kill2")
            {

                Destroy(coll.gameObject);
                killed2 = true;
                possess.level++;

            }
        }
    }

}
