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

    public void ShowNextNoninteractiveLine()
    {
        GameObject objeto = Instantiate(textPrefab, masterPanel);
        Text texto = objeto.GetComponent<Text>();
        // later on animation migth be here or in the prefab itself
        texto.text = nonInteractiveLines[nextDisplayed].textLine;
        // TODO define proper colors
        texto.color = nonInteractiveLines[nextDisplayed].character == Line.Character.Deneb ? Color.red : Color.cyan;
        objeto.GetComponent<RectTransform>().SetAsLastSibling();
        nextDisplayed++;
    }

    public void ShowNextNoninteractiveExitLine()
    {
        GameObject objeto = Instantiate(textPrefab, masterPanel);
        Text texto = objeto.GetComponent<Text>();
        // later on animation migth be here or in the prefab itself
        texto.text = exitLines[nextDisplayedExit].textLine;
        // TODO define proper colors
        texto.color = exitLines[nextDisplayedExit].character == Line.Character.Deneb ? Color.red : Color.cyan;
        objeto.GetComponent<RectTransform>().SetAsLastSibling();
        nextDisplayedExit++;

        if (nextDisplayedExit >= exitLines.Count)
        {
            exiting = true;
        }
    }

    public void ShowNextNoninteractiveChoiceLines()
    {
        GameObject objeto = Instantiate(textPrefab, masterPanel);
        Text texto = objeto.GetComponent<Text>();
        // later on animation migth be here or in the prefab itself
        texto.text = beforeChoiceLines[nextDisplayedChoiceLines].textLine;
        // TODO define proper colors
        texto.color = beforeChoiceLines[nextDisplayedChoiceLines].character == Line.Character.Deneb ? Color.red : Color.cyan;
        objeto.GetComponent<RectTransform>().SetAsLastSibling();
        nextDisplayedChoiceLines++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown){
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
                    texto_P1.text = line_to_P1.textLine;
                    texto_P1.color = Color.red;
                    button1.GetComponent<RectTransform>().SetAsLastSibling();
                    button1.GetComponent<TextButton>().onClick.AddListener(GotoOpt1);

                    button2 = Instantiate(bottonPrefab, masterPanel);
                    Text texto_P2 = button2.GetComponent<Text>();
                    texto_P2.text = line_to_P2.textLine;
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
