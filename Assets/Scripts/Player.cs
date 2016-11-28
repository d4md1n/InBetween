using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Controller2D))]
    public class Player : MonoBehaviour
    {
        public float MaxJumpHeight = 4f;
        public float MinJumpHeight = 1f;
        public float TimeToJumpApex = .4f;
        public bool CanFly;

        public static bool Killed1;
        public static bool Killed2;


        private float accelerationTimeAirborne = .2f;
        private float accelerationTimeGrounded = .1f;
        private float moveSpeed = 6f;

        private float gravity;
        private float maxJumpVelocity;
        private float minJumpVelocity;
        public Vector3 velocity;
        private float velocityXSmoothing;

        private Controller2D controller;
        private Animator moving;
        private Animator stoping;
        private Vector2 directionalInput;


        private void Start()
        {
            AssignComponents();
            CalculateConstants();
        }

        private void CalculateConstants()
        {
            gravity = -(2 * MaxJumpHeight) / Mathf.Pow(TimeToJumpApex, 2);
            maxJumpVelocity = Mathf.Abs(gravity) * TimeToJumpApex;
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * MinJumpHeight);
        }

        private void AssignComponents()
        {
            moving = GetComponent<Animator>();
            controller = GetComponent<Controller2D>();
        }

        private void Update()
        {
            CalculateVelocity();
            controller.Move(velocity * Time.deltaTime, directionalInput);
            ControlPlayerAnimation();

            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0f;
            }
        }

        private void ControlPlayerAnimation()
        {
            if (directionalInput.x != 0)
            {
                moving.SetTrigger("Move");
                if (directionalInput.x == -1) GetComponent<SpriteRenderer>().flipX = true;
                if (directionalInput.x == 1) GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                moving.SetTrigger("Stop");
            }
        }

        private void CalculateVelocity()
        {
            float targetVelocityX = directionalInput.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
                (controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne));
        }

        public void SetDirectionalInput(Vector2 directionalInput)
        {
            this.directionalInput = directionalInput;
        }

        public void OnJumpInputDown()
        {
            if (CanFly || CollisionsBelow())
            {
                velocity.y = maxJumpVelocity;
            }
        }

        public void OnJumpInputUp()
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }

        private bool CollisionsBelow()
        {
            try
            {
                return controller.collisions.below;
            }
            catch
            {
                return true;
            }

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (gameObject.tag == "main" && (collision.gameObject.tag == "Kill1" || collision.gameObject.tag == "Kill2"))
            {
                Destroy(collision.gameObject);
                Possess.level++;
            }
            if (gameObject.tag == "Player")
            {
                if (collision.gameObject.tag == "Kill1")
                {

                    Destroy(collision.gameObject);
                    Killed1 = true;

                }
                if (collision.gameObject.tag == "Kill2")
                {

                    Destroy(collision.gameObject);
                    Killed2 = true;
                    Possess.level++;

                }
            }
        }

    }
}