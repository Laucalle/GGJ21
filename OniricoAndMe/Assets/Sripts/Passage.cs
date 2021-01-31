using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passage : MonoBehaviour
{
    public GameManager manager;
    public List<Line> nonInteractiveLines = new List<Line>();
    public List<Line> exitLines = new List<Line>();
    public List<Line> beforeChoiceLines = new List<Line>();
    int nextDisplayed, nextDisplayedExit, nextDisplayedChoiceLines;
    public RectTransform masterPanel;
    public GameObject textPrefab, bottonPrefab;
    public Line line_to_P1, line_to_P2;
    public Passage option_1, option_2;
    public bool is_win = false;
    private GameObject button1, button2;
    private bool options_instantiated, exiting;
    // Start is called before the first frame update
    void OnEnable()
    {
        options_instantiated = false;
        nextDisplayed = 0;
        nextDisplayedExit = 0;
        nextDisplayedChoiceLines = 0;
        exiting = false;
        manager.active_passage = this;
        if (is_win)
        {
            manager.points++;
        }
    }

    public void GotoOpt1()
    {
        option_1.gameObject.SetActive(true);

        button2.SetActive(false);
        button1.GetComponent<TextButton>().interactive = false;
        LayoutRebuilder.ForceRebuildLayoutImmediate(masterPanel);
        option_1.ShowNextNoninteractiveLine();
        LayoutRebuilder.ForceRebuildLayoutImmediate(masterPanel);

        this.gameObject.SetActive(false);
    }

    public void GotoOpt2()
    {
        option_2.gameObject.SetActive(true);

        button1.SetActive(false);
        button2.GetComponent<TextButton>().interactive = false;
        LayoutRebuilder.ForceRebuildLayoutImmediate(masterPanel);
        option_2.ShowNextNoninteractiveLine();
        LayoutRebuilder.ForceRebuildLayoutImmediate(masterPanel);

        this.gameObject.SetActive(false);
    }

    private string GetCharacterString(Line line)
    {
        string name;
        switch (line.character)
        {
            case Line.Character.Deneb:

                name =  "Deneb: ";
                break;

            case Line.Character.Sirio:

                name = "Sirio: ";
                break;

            case Line.Character.StrangeCreature:

                name = "Strange Creature: ";
                break;

            default:
                name = "";
                break;


        }

        return name;
    }

    public void ShowNextNoninteractiveLine()
    {
        GameObject objeto = Instantiate(textPrefab, masterPanel);
        Text texto = objeto.GetComponent<Text>();
        Line current_line = nonInteractiveLines[nextDisplayed];

        manager.CallAnimation(current_line);
        manager.CallSound(current_line);

        texto.text = current_line.textLine;
        texto.text = GetCharacterString(current_line) + texto.text;
        texto.color = current_line.character == Line.Character.Deneb ? Color.red : Color.cyan;
        objeto.GetComponent<RectTransform>().SetAsLastSibling();
        nextDisplayed++;
    }

    public void ShowNextNoninteractiveExitLine()
    {
        GameObject objeto = Instantiate(textPrefab, masterPanel);
        Text texto = objeto.GetComponent<Text>();
        Line current_line = exitLines[nextDisplayedExit];

        manager.CallAnimation(current_line);
        manager.CallSound(current_line);

        texto.text = current_line.textLine;
        texto.text = GetCharacterString(current_line) + texto.text;
        texto.color = current_line.character == Line.Character.Deneb ? Color.red : Color.cyan;
        objeto.GetComponent<RectTransform>().SetAsLastSibling();
        nextDisplayedExit++;

        if (nextDisplayedExit >= exitLines.Count)
        {
            exiting = true;
        }

        if (nextDisplayedExit >= exitLines.Count - 1)
        {
            manager.player_in_control = false;
        }
    }

    public void ShowNextNoninteractiveChoiceLines()
    {
        GameObject objeto = Instantiate(textPrefab, masterPanel);
        Text texto = objeto.GetComponent<Text>();
        Line current_line = beforeChoiceLines[nextDisplayedChoiceLines];

        manager.CallAnimation(current_line);
        manager.CallSound(current_line);

        texto.text = current_line.textLine;
        texto.text = GetCharacterString(current_line) + texto.text;
        texto.color = current_line.character == Line.Character.Deneb ? Color.red : Color.cyan;
        objeto.GetComponent<RectTransform>().SetAsLastSibling();
        nextDisplayedChoiceLines++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && manager.player_in_control){
            // instanciamos la siguiente linea de dialogo si la hay, si no la opcion.
            if (nextDisplayed < nonInteractiveLines.Count)
            {
                ShowNextNoninteractiveLine();
            }
            else if (exitLines.Count > 0 && manager.branch_visited && nextDisplayedExit < exitLines.Count)
            {
                ShowNextNoninteractiveExitLine();
            }
            else if (!options_instantiated && !exiting) 
            {

                if (beforeChoiceLines.Count > 0 && nextDisplayedChoiceLines < beforeChoiceLines.Count)
                {
                    ShowNextNoninteractiveChoiceLines();
                }
                else
                {
                    options_instantiated = true;
                    button1 = Instantiate(bottonPrefab, masterPanel);
                    Text texto_P1 = button1.GetComponent<Text>();
                    texto_P1.text = "Deneb: " + line_to_P1.textLine;
                    texto_P1.color = Color.red;
                    button1.GetComponent<RectTransform>().SetAsLastSibling();
                    button1.GetComponent<TextButton>().onClick.AddListener(GotoOpt1);

                    button2 = Instantiate(bottonPrefab, masterPanel);
                    Text texto_P2 = button2.GetComponent<Text>();
                    texto_P2.text = "Deneb: " + line_to_P2.textLine;
                    texto_P2.color = Color.red;
                    button2.GetComponent<RectTransform>().SetAsLastSibling();
                    button2.GetComponent<TextButton>().onClick.AddListener(GotoOpt2);

                    if (exitLines.Count > 0)
                    {
                        manager.branch_visited = true;
                    }

                }

            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(masterPanel);
        }
    }
}
