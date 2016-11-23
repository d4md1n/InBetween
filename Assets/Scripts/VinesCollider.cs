using UnityEngine;
using System.Collections;

public class VinesCollider : MonoBehaviour {

    public GameObject Vines;
    bool hide = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (hide) {
            //Vines.transform.Translate(Vector3.up * Time.deltaTime);
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "RollingStone")
        {
            hide = true;
            Destroy(gameObject); //.transform.localScale = new Vector3(0, 0, 0);
        }           

    }
}
