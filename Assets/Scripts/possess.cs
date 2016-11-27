using UnityEngine;

namespace Assets.Scripts
{
    public class Possess : MonoBehaviour {
        public GameObject player;
        public GameObject dog;
        public GameObject bat;
        public GameObject frog;
        GameObject other;
        bool playerActive = true;
        bool dogActive;
        bool frogActive;
        bool batActive;
        bool pressed = false;
        public GameObject defaultCamera;
        public GameObject cameraPlayer;
        public GameObject cameraDog;
        public GameObject cameraFrog;
        public GameObject cameraBat;
        bool batKilled;
        bool frogKilled;
        bool dogKilled;
        public static int level;
        public AudioClip possetionSound;
        public AudioClip batDyingSound;
        public AudioClip dogDyingSound;
        public AudioSource audioSource;
        public ParticleSystem batParticles;
        public ParticleSystem dogParticles;

        // Use this for initialization
        void Start () {
            dogActive = false;
            frogActive = false;
            batActive = false;
            playerActive = true;
            dogKilled = false;
            frogKilled = false;
            batKilled = false;
            level = 1;
            dog.GetComponent<Player>().enabled = false;
            bat.GetComponent<Player>().enabled = false;
            //    dogParticles.Stop();
            //     batParticles.Stop() ;
        }

        // Update is called once per frame
        void Update () {
            if ((!playerActive && Input.GetKeyDown(KeyCode.Z) && !pressed) || Player.Killed1 || Player.Killed2)
            {
                player.transform.position = other.transform.position;
                pressed = true;
                playerActive = true;

                //  frogActive = false;

                level++;
                print(level);
                dog.GetComponent<Player>().enabled = false;
                bat.GetComponent<Player>().enabled = false;
                if (level == 4 || level ==3 || Player.Killed2)
                {
                    dogActive = false;
                    Player.Killed2 = false;
                    switchToPlayer(player, dog, cameraPlayer, cameraPlayer);
                    audioSource.PlayOneShot(dogDyingSound, 1);
                    dogParticles.gameObject.SetActive(true);
                }
                else if (level == 2 || Player.Killed1)
                {
                    batActive = false;
                    Player.Killed1 = false;
                    switchToPlayer(player, bat, cameraPlayer, cameraPlayer);
                    audioSource.PlayOneShot(batDyingSound, 1);
                    batParticles.gameObject.SetActive(true);
                }
                else if (level == 4)
                {
                    //switchToPlayer(player, frog, cameraPlayer);
                }
            }

            if (!dogActive && Input.GetKeyDown(KeyCode.Z) && !pressed && !dogKilled && (level >=2))
            {
                dog.GetComponent<Player>().enabled = true;
                dogKilled = true;
                other = dog;
                pressed = true;
                print("dog");
                dogActive = true;
                playerActive = false;
                player.GetComponent<SpriteRenderer>().color -= new Color(0.5f, 0.5f, 0.5f, 0);
                switchToPlayer(dog, player, cameraDog, cameraPlayer);
                audioSource.PlayOneShot(possetionSound, 1);
            }
            if (!batActive && Input.GetKeyDown(KeyCode.Z) && !pressed && !batKilled && level == 1)
            {
                bat.GetComponent<Player>().enabled = true;
                batKilled = true;
                player.GetComponent<SpriteRenderer>().color -= new Color(0.3f, 0.3f, 0.3f, 0);
                other = bat;
                pressed = true;
                print("bat");
                batActive = true;
                playerActive = false;

                switchToPlayer(bat, player, cameraBat, cameraPlayer);
                audioSource.PlayOneShot(possetionSound, 1);
            }
            //if (!frogActive && Input.GetKeyDown(KeyCode.Z) && !pressed && !frogKilled && level == 3)
            //{
            //    frogKilled = true;
            //    other = frog;
            //    pressed = true;
            //    print("frog");
            //    frogActive = true;
            //    playerActive = false;
            //       frog.GetComponent<Player>().enabled = false;
            //    switchToPlayer(frog, player, cameraFrog);
            //}
            if (Input.GetKeyUp(KeyCode.Z))
            {
                pressed = false;
            }

            if (playerActive)
            {
                //print("aaa");
                moveCamera(cameraPlayer, player);

            }
            if (dogActive)
            {
                moveCamera(cameraDog, dog);
            }
            if (batActive)
            {
                moveCamera(cameraBat, bat);
            }
            if (frogActive)
            {

            }
        }

        private void moveCamera(GameObject cameraLocation, GameObject current)
        {
            defaultCamera.transform.position = cameraLocation.transform.position; //Consider lerping
            defaultCamera.transform.rotation = cameraLocation.transform.rotation; //Consider lerping
            player.transform.position = current.transform.position;
        }
        public int getLevel()
        {
            return level;
        }

        void switchToPlayer(GameObject inputToEnable, GameObject inputToDisable, GameObject cameraLocation, GameObject playerCamera)
        {
            inputToEnable.GetComponent<SpriteRenderer>().enabled=true;
            inputToEnable.GetComponent<BoxCollider2D>().enabled = true;
            inputToDisable.GetComponent<SpriteRenderer>().enabled = false;
            inputToDisable.GetComponent<BoxCollider2D>().enabled = false;
            defaultCamera.transform.position = cameraLocation.transform.position; //Consider lerping
            defaultCamera.transform.rotation = cameraLocation.transform.rotation; //Consider lerping
            //playerCamera.transform.position = cameraLocation.transform.position; //Consider lerping
            // playerCamera.transform.rotation = cameraLocation.transform.rotation; //Consider lerping

        }
    }
}
