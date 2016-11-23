using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
    public Canvas menu;
    public Canvas dialogue;
    public GameObject player;
    public GameObject spirit;
    bool play;
	// Use this for initialization
	void Start () {
        player.GetComponent<Player>().enabled = false;
        dialogue.enabled = false;
        play = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (spirit.transform.position.y > 4 && !play)
        {
            spirit.transform.position -= Vector3.up * Time.deltaTime * 10;
        }
        else if(!play)
        {
            dialogue.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && dialogue.enabled)
        {
            dialogue.enabled = false;
            play = true;
            player.GetComponent<Player>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !dialogue.enabled)
        {
            player.GetComponent<Player>().enabled = !player.GetComponent<Player>().enabled;
            menu.enabled = !menu.enabled;
        }
        if(!dialogue.enabled && play)
        {
            spirit.transform.position += Vector3.up * Time.deltaTime *10;
        }
	}
    public void Quit()
    {
        Application.Quit();
    }
    public void Resume()
    {
        menu.enabled = false;
    }
}
