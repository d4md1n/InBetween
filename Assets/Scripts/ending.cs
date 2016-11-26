using UnityEngine;

namespace Assets.Scripts
{
    public class Ending : MonoBehaviour {
        public GameObject bat;
        public GameObject spirit;
        public GameObject gameManager;
        public GameObject player;
        public Canvas dialogue;
        public GameObject god;
        public GameObject blob;
        public GameObject text;
        public GameObject resultGood;
        public GameObject resultBad;
        bool hit;
        bool bad;
        bool next;
        bool next2;
        bool next3;
        bool up;
        // Use this for initialization
        void Start () {
            bat.SetActive(false);
            spirit.SetActive(false);
            //  player.SetActive(true);
            bad = false;
            next = false;
            next2 = false;
            hit = false;
        }

        // Update is called once per frame
        void Update () {
            if (hit)
            {
                player.GetComponent<Player>().enabled = false;
            }
            if (hit && Input.GetKeyDown(KeyCode.Space) && !next) {
                text.SetActive(false);
                next = true;
                up = false;
            }
            if (bad && next) {
                resultBad.SetActive(true);
                next3 = true;
            }
            if (!bad && next)
            {
                resultGood.SetActive(true);
                next3 = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                up = true;
            }
            if(next3 && Input.GetKeyDown(KeyCode.Space) && up)
            {
                next2 = true;
                dialogue.enabled = false;
            }
            if (dialogue.enabled == false && bad && blob.transform.position.x >82 && next2)
            {
                god.transform.position += Vector3.up * Time.deltaTime * 10;
                bat.SetActive(true);
                player.SetActive(false);
                blob.transform.position -= new Vector3(Time.deltaTime * 4,0,0);
            }

            if (dialogue.enabled == false && !bad && next2)
            {
                god.transform.position += Vector3.up * Time.deltaTime * 10;
                spirit.SetActive(true);
                player.SetActive(false);
                spirit.transform.position += Vector3.up * Time.deltaTime * 8;
            }

        }

        void OnCollisionEnter2D(Collision2D coll)
        {

            if (coll.gameObject.tag == "main")
            {
                hit = true;
                dialogue.enabled =true;
                god.SetActive(true);
                if (gameManager.GetComponent<Possess>().getLevel() >3)
                {
                    bad = true;
                    //  bat.SetActive(true);
                    print("bat");

                }
                else {
                    bad = false;
                    //   bat.SetActive(true);
                    print("spirit");
                }

            }

        }
    }
}
