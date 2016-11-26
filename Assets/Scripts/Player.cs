using System;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Controller2D))]
    public class Player : MonoBehaviour
    {
        public float maxJumpHeight = 4f;
        public float minJumpHeight = 1f;
        public float timeToJumpApex = .4f;
        private float accelerationTimeAirborne = .2f;
        private float accelerationTimeGrounded = .1f;
        private float moveSpeed = 6f;

        public bool canDoubleJump;
        private bool isDoubleJumping = false;

        private float gravity;
        private float maxJumpVelocity;
        private float minJumpVelocity;
        private Vector3 velocity;
        private float velocityXSmoothing;

        private Controller2D controller;
        Animator moving;
        Animator stoping;
        private Vector2 directionalInput;

        public static bool killed1;
        public static bool killed2;

        private void Start()
        {
            print("entered start function Player");
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

            //print("entered updated function in Player");
            CalculateVelocity();
            controller.Move(velocity * Time.deltaTime, directionalInput);
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                print("player Right pressed");
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

        private void CalculateVelocity()
        {
            float targetVelocityX = directionalInput.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne));
            velocity.y += gravity * Time.deltaTime;
        }

        public void SetDirectionalInput(Vector2 input)
        {

            directionalInput = input;
        }

        public void OnJumpInputDown()
        {
            if (CollisionsBelow())
            {
                velocity.y = maxJumpVelocity;
                isDoubleJumping = false;
            }
            if (canDoubleJump && !CollisionsBelow() && !isDoubleJumping) //&& !wallSliding)
            {
                velocity.y = maxJumpVelocity;
                isDoubleJumping = false;
            }
        }

        private bool CollisionsBelow()
        {
            try
            {
                return controller.collisions.below;
            }
            catch (Exception)
            {
                return true;
            }

        }

        public void OnJumpInputUp()
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (gameObject.tag == "main" && (coll.gameObject.tag == "Kill1" || coll.gameObject.tag == "Kill2"))
            {
                Destroy(coll.gameObject);
                Possess.level++;
            }
            if (gameObject.tag == "Player")
            {
                if (coll.gameObject.tag == "Kill1")
                {

                    Destroy(coll.gameObject);
                    killed1 = true;

                }
                if (coll.gameObject.tag == "Kill2")
                {

                    Destroy(coll.gameObject);
                    killed2 = true;
                    Possess.level++;

                }
            }
        }

    }
}
