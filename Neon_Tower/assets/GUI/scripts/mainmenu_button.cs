using UnityEngine;
using System.Collections;

public class mainmenu_button : MonoBehaviour {

    public GameObject leftArrow;

    private Vector2 boxSize, lowerleft, upperright, mpos;
    private Vector2 bboxSize, blowerleft, bupperright, tsize, csize;
    private Vector3 start, end, start1, end1;
    private bool pressed, animate, bend1, levelselect;
    private float lerpTime, lerpPosition, lerpTime2;

	// Use this for initialization
    void Start()
    {
        Awake();
    }

	void Awake () {

        boxSize = ((BoxCollider2D)(GetComponent<BoxCollider2D>())).size;
        lowerleft = new Vector2(transform.position.x - boxSize.x, transform.position.y - boxSize.y);
        upperright = new Vector2(transform.position.x + boxSize.x, transform.position.y + boxSize.y);

        bboxSize = ((BoxCollider2D)(leftArrow.GetComponent<BoxCollider2D>())).size;
        blowerleft = new Vector2(leftArrow.transform.position.x - bboxSize.x, leftArrow.transform.position.y - bboxSize.y);
        bupperright = new Vector2(leftArrow.transform.position.x + bboxSize.x, leftArrow.transform.position.y + bboxSize.y);

        start = new Vector3(0, 0, -10f);
        start1 = new Vector3(0, 2f, -10f);
        end = new Vector3(0, -10f, -10f);
        end1 = new Vector3(0, -12f, -10f);

        pressed = false;
        animate = false;
        bend1 = false;
        levelselect = false;


        lerpTime2 = 0.2f;
        lerpPosition = 0.0f;
        lerpTime = 0.5f;
        //lerpTime2 = ((end1.x - end.x) * lerpTime / end.x) * 2.5f;

        //csize = (new Vector2(Screen.currentResolution.width, Screen.currentResolution.height));
        //tsize = Camera.main.camera.ScreenToWorldPoint(new Vector2(((1) * csize.x / 5f) - (csize.x / 10f), -leftArrow.GetComponent<BoxCollider2D>().size.y*20));
        //leftArrow.transform.position = tsize;

        leftArrow.transform.localScale = new Vector3(1f, 1f, 1f);
	}


    void Update()
    {
        if (animate)
        {
            if (!bend1)
            {
                lerpPosition += Time.deltaTime / lerpTime;
                if (levelselect)
                    Camera.main.transform.position = Vector3.Lerp(start, end1, lerpPosition);
                else
                    Camera.main.transform.position = Vector3.Lerp(end, start1, lerpPosition);
                if (lerpPosition >= 1)
                {
                    bend1 = true;
                    lerpPosition = 0;
                }
                
            }
            else
            {
                
                lerpPosition += Time.deltaTime / lerpTime2;
                if (levelselect)
                    Camera.main.transform.position = Vector3.Lerp(end1, end, lerpPosition);
                else
                    Camera.main.transform.position = Vector3.Lerp(start1, start, lerpPosition);
                if (lerpPosition >= 1)
                {
                    levelselect = !levelselect;
                    bend1 = false;
                    animate = false;
                    lerpPosition = 0;
                }
            }
        }
        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mpos.x >= lowerleft.x && mpos.x <= upperright.x && mpos.y >= lowerleft.y && mpos.y <= upperright.y)
                {
                    transform.localScale = new Vector3(1.2f, 1.2f, 1f);
                    pressed = true;
                }
                if (mpos.x >= blowerleft.x && mpos.x <= bupperright.x && mpos.y >= blowerleft.y && mpos.y <= bupperright.y)
                {
                    leftArrow.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
                    pressed = true;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (mpos.x >= lowerleft.x && mpos.x <= upperright.x && mpos.y >= lowerleft.y && mpos.y <= upperright.y)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    pressed = false;
                    animate = true;
                    bend1 = false;
                    levelselect = true;
                    lerpPosition = 0;
                }

                if (mpos.x >= blowerleft.x && mpos.x <= bupperright.x && mpos.y >= blowerleft.y && mpos.y <= bupperright.y)
                {
                    leftArrow.transform.localScale = new Vector3(1f, 1f, 1f);
                    pressed = false;
                    animate = true;
                    bend1 = false;
                    levelselect = false;
                    lerpPosition = 0;
                }
            }
            
            
        }
    }

	// Update is called once per frame
	//void Update () {
        //if (Input.touchCount == 1) 
        //{
        //    Debug.Log("touchCount");
        //    if (Input.GetTouch(0).position.x >= lowerleft.x && Input.GetTouch(0).position.x <= upperright.x && Input.GetTouch(0).position.y >= lowerleft.y && Input.GetTouch(0).position.y <= upperright.y)
        //    {
        //        Debug.Log("inside");
        //        if (Input.GetTouch(0).phase == TouchPhase.Began)
        //        {
        //            transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        //            pressed = true;
        //            Debug.Log("Touchyneg");
        //        }
        //        else if (Input.GetTouch(0).phase == TouchPhase.Ended)
        //        {
        //            if (pressed)
        //            {
        //                pressed = false;
        //                transform.localScale = new Vector3(1f, 1f, 1f);
        //                Application.LoadLevel("Tower02");
        //                Debug.Log("Touchy");
        //            }
        //        }
        //    }
        //}

        /// Direct play
        //if (Input.GetMouseButtonDown(0))
        //{
        //    mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    if (mpos.x >= lowerleft.x && mpos.x <= upperright.x && mpos.y >= lowerleft.y && mpos.y <= upperright.y)
        //    {
                
        //        transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        //        pressed = true;
        //    }
        //}
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    if (mpos.x >= lowerleft.x && mpos.x <= upperright.x && mpos.y >= lowerleft.y && mpos.y <= upperright.y)
        //    {
        //        transform.localScale = new Vector3(1f, 1f, 1f);
        //        pressed = false;
        //        Application.LoadLevel("Tower02");
        //    }
        //}



	//}
}
