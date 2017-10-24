using UnityEngine;
using System.Collections;

public class ControlsCheckController : MonoBehaviour {

    public int controlNumber;
    public Sprite blueSlot, greySlot;

    private Vector2 mPos, boxSize, lowerleft, upperright;
    private bool selectedc;

	// Use this for initialization
	void Start () {
        ((SpriteRenderer)(GetComponent<SpriteRenderer>())).sprite = greySlot;
        selectedc = false;
        if (PlayerPrefs.HasKey("gameControls"))
        {
            if (PlayerPrefs.GetInt("gameControls") == controlNumber)
            {
                ((SpriteRenderer)(GetComponent<SpriteRenderer>())).sprite = blueSlot;
                selectedc = true;
            }
        }
        else
        {
            if (controlNumber == 0)
            {
                PlayerPrefs.SetInt("gameControls", 0);
                ((SpriteRenderer)(GetComponent<SpriteRenderer>())).sprite = blueSlot;
                selectedc = true;
            }
        }

        boxSize = ((BoxCollider2D)(GetComponent<BoxCollider2D>())).size;
        lowerleft = new Vector2(transform.position.x - boxSize.x/2, transform.position.y - boxSize.y/2);
        upperright = new Vector2(transform.position.x + boxSize.x/2, transform.position.y + boxSize.y/2);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (mPos.x >= lowerleft.x && mPos.x <= upperright.x && mPos.y >= lowerleft.y && mPos.y <= upperright.y)
            {
                Debug.Log("asdfasdfasdf123123");
                PlayerPrefs.SetInt("gameControls", controlNumber);
                ((SpriteRenderer)(GetComponent<SpriteRenderer>())).sprite = blueSlot;
            }
        }

        if (PlayerPrefs.GetInt("gameControls") == controlNumber)
        {
            if (!selectedc)
            {
                ((SpriteRenderer)(GetComponent<SpriteRenderer>())).sprite = blueSlot;
                selectedc = true;
            }
        }
        else
        {
            if (selectedc)
            {
                ((SpriteRenderer)(GetComponent<SpriteRenderer>())).sprite = greySlot;
                selectedc = false;
            }
        }
	}
}


//Debug.Log("mPos:");
//            Debug.Log(mPos);
//            Debug.Log("ll:");
//            Debug.Log(lowerleft);
//            Debug.Log("ur   :");
//            Debug.Log(upperright);


            //Debug.Log(mPos.x + " == " + lowerleft.x);
            //Debug.Log(mPos.x + " == " + upperright.x);
            //Debug.Log(mPos.y + " == " + lowerleft.y);
            //Debug.Log(mPos.y + " == " + upperright.y);