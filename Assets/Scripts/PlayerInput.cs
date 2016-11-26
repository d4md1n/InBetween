using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Player))]
    public class PlayerInput : MonoBehaviour
    {
        private Player player;

        private void Start()
        {
            print("entered start function Player Input");
            player = GetComponent<Player>();

        }

        private void Update()
        {
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            player.SetDirectionalInput(directionalInput);

            //print("test");

            if (Input.GetButtonDown("Jump"))
            {
                player.OnJumpInputDown();
            }

            if (Input.GetButtonUp("Jump"))
            {
                player.OnJumpInputUp();
            }
        }
    }
}
