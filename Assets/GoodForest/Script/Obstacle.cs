using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public Vector2 velocity = new Vector2(-4, 0);
    public Vector2 range = new Vector2(0,0);
    public bool close;
    public bool apply;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        transform.position = new Vector3(transform.position.x, Random.Range(range.x, range.y), transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < -22)
        {
            Destroy(this.gameObject);
        }


        if (!apply && GameManager.manager.activeAider_B)
        {
            GetComponent<Animator>().CrossFade("Open", 0.5F);
            apply = true;
        }
        else if (!close && !GameManager.manager.activeAider_B && !GameManager.manager.applyAyudante)
        {
            GetComponent<Animator>().CrossFade("Close", 0.5F);
            close = true;
        }
	}
}
