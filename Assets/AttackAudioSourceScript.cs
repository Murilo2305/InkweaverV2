using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAudioSourceScript : MonoBehaviour
{

    [SerializeField] AudioSource LAttack1, LAttack2, LAttack3, HAttack;
    void Start()
    {
        LAttack1 = transform.GetChild(0).GetComponent<AudioSource>();
        LAttack2 = transform.GetChild(1).GetComponent<AudioSource>();
        LAttack3 = transform.GetChild(2).GetComponent<AudioSource>();
        HAttack = transform.GetChild(3).GetComponent<AudioSource>();
    }

    public void PlayAttackSoundEffect(int id)
    {
        if(Time.timeScale != 0.0f)
        {
            switch (id)
            {
                case 1:
                    LAttack1.Play();
                    break;
                case 2:
                    LAttack2.Play();
                    break;
                case 3:
                    LAttack3.Play();
                    break;
                case 4:
                    HAttack.Play();
                    break;
            }
        }
    }
}
