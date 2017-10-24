using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{

    public GameObject settingsButton, leftArrow, levelCard, upArrow;
    
    private GameObject[][] levelCards;
    private Vector3 start, end, end1, start1;
    private Vector2 sboxSize, slowerleft, supperright, mpos;
    private Vector2 bboxSize, blowerleft, bupperright;
    private Vector2 llowerleft, lupperright, csize, tsize, cardBoxSize;
    private bool pressed, settings, animate, bend1;
    private float targetx, lerpPosition, lerpTime, lerpTime2, aspect;
    private int gameControls;

    void Start()
    {
        Awake();
    }

    // Use this for initialization
    void Awake()
    {
        sboxSize = ((BoxCollider2D)(settingsButton.GetComponent<BoxCollider2D>())).size;
        slowerleft = new Vector2(settingsButton.transform.position.x - sboxSize.x / 2, settingsButton.transform.position.y - sboxSize.y / 2);
        supperright = new Vector2(settingsButton.transform.position.x + sboxSize.x / 2, settingsButton.transform.position.y + sboxSize.y / 2);

        bboxSize = ((BoxCollider2D)(leftArrow.GetComponent<BoxCollider2D>())).size;
        blowerleft = new Vector2(leftArrow.transform.position.x - bboxSize.x / 2, leftArrow.transform.position.y - bboxSize.y / 2);
        bupperright = new Vector2(leftArrow.transform.position.x + bboxSize.x / 2, leftArrow.transform.position.y + bboxSize.y / 2);

        cardBoxSize = ((BoxCollider2D)(levelCard.GetComponent<BoxCollider2D>())).size;

        aspect = (Screen.currentResolution.width / Screen.currentResolution.height);

        //csize = new Vector2(Camera.main.camera.ScreenToWorldPoint(), Camera.main.camera.orthographicSize);
        csize =(new Vector2(Screen.currentResolution.width*0.9f, Screen.currentResolution.height*0.9f));
        
        pressed = false;
        settings = false;
        animate = false;

        start = new Vector3(0, 0, -10);
        start1 = new Vector3(-2, 0, -10);
        end = new Vector3(21, 0, -10);
        end1 = new Vector3(23f, 0, -10);
        bend1 = false;

        targetx = 21f;
        lerpPosition = 0.0f;
        lerpTime = 0.5f;
        lerpTime2 = 0.2f;
        //lerpTime2 = ((end1.x - end.x) * lerpTime / end.x) * 2.5f;

        setupLevels();
   } 

    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            if (!bend1)
            {
                lerpPosition += Time.deltaTime / lerpTime;
                if (settings)
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
                if (settings)
                    Camera.main.transform.position = Vector3.Lerp(end1, end, lerpPosition);
                else
                    Camera.main.transform.position = Vector3.Lerp(start1, start, lerpPosition);
                if (lerpPosition >= 1)
                {
                    settings = !settings;
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
                if (mpos.x >= slowerleft.x && mpos.x <= supperright.x && mpos.y >= slowerleft.y && mpos.y <= supperright.y)
                {
                    settingsButton.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
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
                if (mpos.x >= slowerleft.x && mpos.x <= supperright.x && mpos.y >= slowerleft.y && mpos.y <= supperright.y)
                {
                    settingsButton.transform.localScale = new Vector3(1f, 1f, 1f);
                    pressed = false;
                    animate = true;
                    bend1 = false;
                    settings = true;
                    lerpPosition = 0;
                }

                if (mpos.x >= blowerleft.x && mpos.x <= bupperright.x && mpos.y >= blowerleft.y && mpos.y <= bupperright.y)
                {
                    leftArrow.transform.localScale = new Vector3(1f, 1f, 1f);
                    pressed = false;
                    animate = true;
                    bend1 = false;
                    settings = false;
                    lerpPosition = 0;
                }
            }
        }
    }

    // Setup Settings

    void setupSettings()
    {
        if (PlayerPrefs.HasKey("gameControls"))
        {
            gameControls = PlayerPrefs.GetInt("gameControls");
        }
        else
        {
            PlayerPrefs.SetInt("gameControls", 1);
            gameControls = PlayerPrefs.GetInt("gameControls");
        }
    }

    void setupLevels()
    {
        //this.transform.position.y+Camera.main.ScreenToWorldPoint(new Vector3(0, -Camera.main.orthographicSize, 0)).y+(player.collider2D as BoxCollider2D).size.y/2f);

        //levelCard.transform.position = new Vector3(csize.x, csize.y, 0);
        //levelCard = (Instantiate(levelCard) as GameObject);
        //Instantiate(levelCard, new Vector3(, 0), Quaternion.identity);

        tsize = Camera.main.camera.ScreenToWorldPoint(new Vector2(0, 0)); //(1) * csize.x / 5f) - (csize.x / 10f)
        tsize = new Vector2(tsize.x + upArrow.GetComponent<BoxCollider2D>().size.x, tsize.y - upArrow.GetComponent<BoxCollider2D>().size.y/2);
        //-Screen.currentResolution.height + (csize.y - upArrow.GetComponent<BoxCollider2D>().size.y/4))
            //-Screen.currentResolution.height + 2 * upArrow.GetComponent<BoxCollider2D>().size.y + 0 * (upArrow.GetComponent<BoxCollider2D>().size.y - (upArrow.GetComponent<BoxCollider2D>().size.y / 4f))));
        upArrow.transform.position = tsize;

        levelCards = new GameObject[10][];

        for (int j = 0; j < 2; j++)
        {
            levelCards[j] = new GameObject[10];
            for (int i = 0; i < 5; i++)
            {
                levelCards[j][i] = levelCard;
            }
        }

        int ij = 1;

        for (int j = 1; j >= 0; j--)
        {
            for (int i = 0; i < 5; i++)
            {
                tsize = Camera.main.camera.ScreenToWorldPoint(new Vector2(Screen.currentResolution.width * 0.05f + ((i + 1) * csize.x / 5f) - (csize.x / 10f), -Screen.currentResolution.height + (((j + 1) * csize.y / 2f) - (csize.y / 4f))));
                levelCards[j][i].transform.position = new Vector3(tsize.x, tsize.y);
                levelCards[j][i] = (Instantiate(levelCard) as GameObject);
                
                if(ij < 10)
                    levelCards[j][i].GetComponent<LevelTileController>().levelName = "Tower0" + (ij.ToString());
                else
                    levelCards[j][i].GetComponent<LevelTileController>().levelName = "Tower" + (ij.ToString());
                ij++;
            }
        }
    }

}
