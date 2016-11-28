using Assets.Scripts;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor
{
    [TestFixture]
    public class PlayerSpec
    {
        [Test]
        public void PlayerJumpInputDown()
        {

            GameObject player = new GameObject("player", typeof(Player), typeof(Controller2D));
            player.GetComponent<Controller2D>().collisions.below = true;
            Player playerScript = player.GetComponent<Player>();
            playerScript.CanFly = true;

            playerScript.OnJumpInputDown();

            Assert.AreEqual(20, playerScript.velocity.y);
        }

        [Test]
        public void PlayerJumpInputUp()
        {
            Player player = new Player {velocity = {y = 200}};
            player.OnJumpInputUp();

            Assert.AreEqual(10, player.velocity.y);
        }

    }
}