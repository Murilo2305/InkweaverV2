using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAfterImageScript : MonoBehaviour
{
    [SerializeField] GameObject PlayerRef;
    [SerializeField] SpriteRenderer PlayerSR;
    [SerializeField] Animator PlayerAnim;
    [SerializeField] List<GameObject> DashFrames;
    [SerializeField] float FirstFrameOffSet,SecondFrameOffSet,ThirdFrameOffSet;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");   
        PlayerSR = PlayerRef.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
        PlayerAnim = PlayerRef.transform.GetChild(2).gameObject.GetComponent<Animator>();
    
    }

    // Update is called once per frame
    void Update()
    {   

        for (int i = 0; i < DashFrames.Count; i++)
        {
            if(PlayerAnim.GetBool("IsDashing") == true && !PlayerAnim.GetBool("IsDying"))
            {
                if (i == 0)
                {
                    if (PlayerSR.flipX == false)
                    {
                        DashFrames[i].SetActive(true);
                        DashFrames[i].GetComponent<SpriteRenderer>().flipX = false;
                        DashFrames[i].gameObject.transform.position = new Vector3(PlayerRef.transform.position.x - FirstFrameOffSet, transform.position.y, transform.position.z);
                    }
                    else if (PlayerSR.flipX == true)
                    {
                        DashFrames[i].SetActive(true);
                        DashFrames[i].GetComponent<SpriteRenderer>().flipX = true;
                        DashFrames[i].gameObject.transform.position = new Vector3(PlayerRef.transform.position.x + FirstFrameOffSet, transform.position.y, transform.position.z);
                    }
                }

                if (i == 1)
                {
                    if (PlayerSR.flipX == false)
                    {
                        DashFrames[i].SetActive(true);
                        DashFrames[i].GetComponent<SpriteRenderer>().flipX = false;
                        DashFrames[i].gameObject.transform.position = new Vector3(PlayerRef.transform.position.x - SecondFrameOffSet, transform.position.y, transform.position.z);
                    }
                    else if (PlayerSR.flipX == true)
                    {
                        DashFrames[i].SetActive(true);
                        DashFrames[i].GetComponent<SpriteRenderer>().flipX = true;
                        DashFrames[i].gameObject.transform.position = new Vector3(PlayerRef.transform.position.x + SecondFrameOffSet, transform.position.y, transform.position.z);
                    }
                }

                if (i == 2)
                {
                    if (PlayerSR.flipX == false)
                    {
                        DashFrames[i].SetActive(true);
                        DashFrames[i].GetComponent<SpriteRenderer>().flipX = false;
                        DashFrames[i].gameObject.transform.position = new Vector3(PlayerRef.transform.position.x - ThirdFrameOffSet, transform.position.y, transform.position.z);
                    }
                    else if (PlayerSR.flipX == true)
                    {
                        DashFrames[i].SetActive(true);
                        DashFrames[i].GetComponent<SpriteRenderer>().flipX = true;
                        DashFrames[i].gameObject.transform.position = new Vector3(PlayerRef.transform.position.x + ThirdFrameOffSet, transform.position.y, transform.position.z);
                    }
                }

                if (i == 3)
                {
                    i = 0;
                }
            }else
            {

                DashFrames[i].SetActive(false);

            }
        }
    }
}