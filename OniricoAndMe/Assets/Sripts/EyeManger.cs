using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeManger : MonoBehaviour
{
    public List<Animator> ani_eyes;
    public AudioSource AS;
 
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }
    
    public void WakeUp()
    {
        AS.Play();
        foreach(Animator an in ani_eyes)
        {
            an.speed = Random.Range(0.8f, 1.2f);
            an.gameObject.SetActive(true);
        }
    }
    public void MakeHalfGo()
    {
        int half = ani_eyes.Count / 2;
        int gone = 0;
        foreach(Animator an in ani_eyes)
        {
            if (an.gameObject.activeSelf)
            {
                an.gameObject.SetActive(false);
                gone++;
                if (gone > half)
                {
                    break;
                }
            }
        }
        AS.Play();

    }

}
