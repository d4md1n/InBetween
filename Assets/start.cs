using UnityEngine;
using System.Collections;

public class start : MonoBehaviour {

	// Use this for initialization
	void Start () {
	  
	}

    public void Quit()
    {
        Application.Quit();
    }
    public void Play()
    {
        Application.LoadLevel("demo");
    }
    // Update is called once per frame
    void Update () {
	
	}
}
