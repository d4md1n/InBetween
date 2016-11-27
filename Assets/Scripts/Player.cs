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
        private Vector3 velocity;
        private float velocityXSmoothing;

        private Controller2D controller;
        private Animator moving;
        private Animator stoping;
        private Vector2 directionalInput;


        private void Start()
        {
            moving = GetComponent<Animator>();
            controller = GetComponent<Controller2D>();
            gravity = -(2 * MaxJumpHeight) / Mathf.Pow(TimeToJumpApex, 2);
            maxJumpVelocity = Mathf.Abs(gravity) * TimeToJumpApex;
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * MinJumpHeight);

        }

        private void Update()
        {
            CalculateVelocity();
            controller.Move(velocity * Time.deltaTime, directionalInput);
            ControlPlayerAnimation();
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
            velocity.y += gravity * Time.deltaTime;
        }

        public void SetDirectionalInput(Vector2 directionalInput)
        {
            this.directionalInput = directionalInput;
        }

        public void OnJumpInputDown()
        {
            var variable = CanFly || controller.collisions.below;
            if (variable)
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
                    Killed1 = true;

                }
                if (coll.gameObject.tag == "Kill2")
                {

                    Destroy(coll.gameObject);
                    Killed2 = true;
                    Possess.level++;

                }
            }
        }

    }
}