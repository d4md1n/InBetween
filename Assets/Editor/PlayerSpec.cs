using Assets.Scripts;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor
{
    [TestFixture]
    public class PlayerSpec
    {
        private GameObject player;
        private Player playerScript;
        [SetUp]
        public void Init()
        {
            player = new GameObject("player", typeof(Player), typeof(Controller2D));
            player.GetComponent<Controller2D>().collisions.below = true;
            playerScript = player.GetComponent<Player>();
            playerScript.CanFly = false;

            playerScript.Start();
        }

        [Test]
        public void PlayerJumpInputDown()
        {
            playerScript.OnJumpInputDown();

            Assert.AreEqual(-49.9999962f, playerScript.gravity);
            Assert.AreEqual(19.9999981f, playerScript.velocity.y);
        }

        [Test]
        public void PlayerJumpInputUp()
        {
            playerScript.velocity.y = playerScript.maxJumpVelocity;
            playerScript.OnJumpInputUp();

            Assert.AreEqual(10, playerScript.velocity.y);
        }

    }
}