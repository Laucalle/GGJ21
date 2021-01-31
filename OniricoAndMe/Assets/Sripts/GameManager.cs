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
    public float max_time = 0;
    private bool final_line_printed = false;

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

                    break;

                case Line.SirioAnims.Hand:

                    break;

                case Line.SirioAnims.SmileHand:

                    break;

                case Line.SirioAnims.Nod:

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

                    break;

                case Line.DenebAnims.Happy:

                    break;

                case Line.DenebAnims.Sad:

                    break;

                case Line.DenebAnims.Surprised:

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

                break;

            case Line.SirioSounds.Oh:

                break;

            case Line.SirioSounds.Umm:

                break;

            case Line.SirioSounds.Hehe:

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
                Line aux_line = new Line();
                aux_line.character = Line.Character.Deneb;
                aux_line.deneb_anim = Line.DenebAnims.InOut;
                CallAnimation(aux_line);
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
