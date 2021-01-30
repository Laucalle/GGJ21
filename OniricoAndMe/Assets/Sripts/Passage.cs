using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passage : MonoBehaviour
{
    public List<Line> nonInteractiveLines = new List<Line>();
    int nextDisplayed = 0;
    public RectTransform masterPanel;
    public GameObject textPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown){
            // instanciamos la siguiente linea de dialogo si la hay, si no la opcion.
            if (nextDisplayed < nonInteractiveLines.Count - 1)
            {
                Text texto = Instantiate(textPrefab, masterPanel).GetComponent<Text>();
                // later on animation migth be here or in the prefab itself
                texto.text = nonInteractiveLines[nextDisplayed].textLine;
                // TODO define proper colors
                texto.color = nonInteractiveLines[nextDisplayed].character == Line.Character.Deneb ? Color.red : Color.cyan;

                nextDisplayed++;
            }
            else {
                // instanciamos la opcion 
                // si no ha sido instanciada
                // else no hago nada
            }
        }
    }
}
