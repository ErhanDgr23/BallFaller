using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class kapsc : MonoBehaviour
{
    public int value;
    public int maxvalue;

    public Color color;

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI imagetext;
    [SerializeField] Color[] colors;
    [SerializeField] float[] values;

    float currentvalue;

    private void Start()
    {
        if (value >= 0 && value < values.Length)
        {
            currentvalue = values[value];
            imagetext.text = values[value] + "X";
        }
        else
        {
            // Geçersiz endeks durumunda son elemanı al
            int lastIndex = values.Length - 1;
            currentvalue = values[lastIndex];
            imagetext.text = values[lastIndex] + "X";
        }

        if(value < colors.Length)
            image.color = colors[value];
        else
            image.color = colors[colors.Length - 1];

        color = image.color;
    }
    
    public float takekapvalue()
    {
        return values[value];
    }
}
