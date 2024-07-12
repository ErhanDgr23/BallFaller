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

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI imagetext;
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
    }

    private void LateUpdate()
    {
        float normalizedValue = Mathf.Clamp01((float)value / 15f);
        float opacity = Mathf.Lerp(0.65f, 1.0f, Mathf.Abs(normalizedValue - 0.5f) * 2);
        Color color = Color.Lerp(Color.red, Color.green, Mathf.Abs(normalizedValue - 0.4f) * 2);
        color.a = opacity;
        image.color = color;
    }
    
    public float takekapvalue()
    {
        return values[value];
    }
}
