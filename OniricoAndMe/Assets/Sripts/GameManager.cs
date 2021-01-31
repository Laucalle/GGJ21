using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Passage active_passage;
    public Button boton1, boton2;
    public Color option_color;
    public bool branch_visited, player_in_control;
    public int points;
    public GameObject final_panel, intro_panel;
    private float timer = 0;
    public float max_time = 0, max_pitch = 1, min_pitch = 0;
    private bool final_line_printed = false;
    public RectTransform masterPanel;
    public Animator sirio_anim, deneb_anim;
    public List<AudioClip> sirio_sounds, deneb_sounds;
    public AudioSource mainAudio;

    public void SelectBranch(int option)
    {

        Passage old_passage = active_passage;
        if (option == 1)
        {
            active_passage = active_passage.option_1;
        } else
        {
            active_passage = active_passage.option_2;
        }
        old_passage.gameObject.SetActive(false);
        active_passage.gameObject.SetActive(false);
    }

    public void SetButtonText(string texto, int button_id)
    {
        if (button_id == 1)
        {
            Text texto_boton = boton1.gameObject.GetComponentInChildren<Text>();
            texto_boton.text = texto;
            texto_boton.color = option_color;
            boton1.gameObject.SetActive(true);
            boton1.GetComponent<RectTransform>().SetAsLastSibling();
        } else
        {
            Text texto_boton = boton2.gameObject.GetComponentInChildren<Text>();
            texto_boton.text = texto;
            texto_boton.color = option_color;
            boton2.gameObject.SetActive(true);
            boton2.GetComponent<RectTransform>().SetAsLastSibling();
        }
    }

    public void CallAnimation(Line line)
    {
        if (line.character == Line.Character.Sirio || line.character == Line.Character.StrangeCreature)
        {

            switch (line.sirio_anim)
            {
                case Line.SirioAnims.Smile:
                    sirio_anim.SetTrigger("Smile");
                    break;

                case Line.SirioAnims.Hand:
                    sirio_anim.SetTrigger("Hand");
                    break;

                case Line.SirioAnims.SmileHand:
                    sirio_anim.SetTrigger("SmaileHand");
                    break;

                case Line.SirioAnims.Nod:
                    sirio_anim.SetTrigger("Nod");
                    break;

                default:
                    break;
            }

        }
        else if (line.character == Line.Character.Deneb)
        {
            switch (line.deneb_anim)
            {
                case Line.DenebAnims.InOut:
                    deneb_anim.SetTrigger("WalkIn");
                    break;

                case Line.DenebAnims.Happy:
                    deneb_anim.SetTrigger("Up");
                    break;

                case Line.DenebAnims.Sad:
                    deneb_anim.SetTrigger("Down");
                    break;

                case Line.DenebAnims.Surprised:
                    deneb_anim.SetTrigger("Surprised");
                    break;

                default:
                    break;


            }
        }
    }

    public void CallSound(Line line)
    {

        switch (line.sirio_sound)
        {
            case Line.SirioSounds.Ah:
                mainAudio.pitch = Random.Range(min_pitch, max_pitch);
                mainAudio.PlayOneShot(sirio_sounds[0]);
                break;

            case Line.SirioSounds.Oh:
                mainAudio.pitch = Random.Range(min_pitch, max_pitch);
                mainAudio.PlayOneShot(sirio_sounds[1]);
                break;

            case Line.SirioSounds.Umm:
                mainAudio.pitch = Random.Range(min_pitch, max_pitch);
                mainAudio.PlayOneShot(sirio_sounds[2]);
                break;

            case Line.SirioSounds.Hehe:
                mainAudio.pitch = Random.Range(min_pitch, max_pitch);
                mainAudio.PlayOneShot(sirio_sounds[3]);
                break;

            default:
                break;

        }

        switch(line.deben_sound)
        {
            case Line.DenebSounds.StepsIn:
                mainAudio.PlayOneShot(sirio_sounds[0]);
                break;

            case Line.DenebSounds.StepsOut:
                mainAudio.PlayOneShot(sirio_sounds[1]);
                break;

            default:
                break;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        branch_visited = false;
        player_in_control = true;
        points = 0;
        mainAudio = gameObject.GetComponent<AudioSource>();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator WaitAndInEnable(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        intro_panel.SetActive(false);

    }

    public void CallWaitAndEnable(float waitTime)
    {
        StartCoroutine(WaitAndInEnable(waitTime));
    }

    // Update is called once per frame
    void Update()
    {

        if (!player_in_control)
        {

            if (timer == 0)
            {
                deneb_anim.SetTrigger("WalkIn");
                timer += Time.deltaTime;

            } else
            {
                timer += Time.deltaTime;
            }
            
            if (timer >= max_time)
            {
                if (!final_line_printed)
                {
                    active_passage.ShowNextNoninteractiveExitLine();
                    LayoutRebuilder.ForceRebuildLayoutImmediate(masterPanel);
                    final_line_printed = true;

                } else if (Input.anyKeyDown)
                {
                    final_panel.SetActive(true);

                    switch (points)
                    {
                        case 1:
                            final_panel.transform.GetChild(0).GetComponent<Text>().text = "Ending 1";
                            break;
                        case 2:
                            final_panel.transform.GetChild(0).GetComponent<Text>().text = "Ending 2";
                            break;
                        default:
                            final_panel.transform.GetChild(0).GetComponent<Text>().text = "Ending 3";
                            break;

                    }
                    
                }
            }

        }
    }
}
