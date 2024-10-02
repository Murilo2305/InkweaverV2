using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVignete_Script : MonoBehaviour
{

    [SerializeField] Animator VigneteAnim;

    // Start is called before the first frame update
    void Start()
    {

        VigneteAnim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fade()
    {

        VigneteAnim.SetTrigger("FadeTrigger");

    }

}
