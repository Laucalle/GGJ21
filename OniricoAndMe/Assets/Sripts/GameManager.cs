﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Passage active_passage;
    public Button boton1, boton2;
    public Color option_color;
    public bool branch_visited;
    public int points;

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

    // Start is called before the first frame update
    void Start()
    {
        branch_visited = false;
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
