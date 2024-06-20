using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
public class showKeyboard : MonoBehaviour
{

    private TMP_InputField input_field;
    public float distance = 0.5f;
    public float verticalOffset = -0.5f;
    public Transform positionSource;

    void Start()
    {
        input_field = gameObject.GetComponent<TMP_InputField>();
        input_field.onSelect.AddListener(x=> openKeyboard());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public  void openKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = input_field;
        NonNativeKeyboard.Instance.PresentKeyboard(input_field.text);

        Vector3 direction = positionSource.forward;
        direction.y = 0;
        direction.Normalize();
        Vector3 targetPosition = positionSource.position + direction * distance +Vector3.up * verticalOffset;
        NonNativeKeyboard.Instance.RepositionKeyboard(targetPosition);
        setCaretColor(1);
        NonNativeKeyboard.Instance.OnClosed += KeyboardClose_CaretCOlor;
    }

    private void KeyboardClose_CaretCOlor(object sender, System.EventArgs e)
    {
        setCaretColor(0);
        NonNativeKeyboard.Instance.OnClosed -= KeyboardClose_CaretCOlor;

    }

    public void setCaretColor(float value)
    {
        input_field.customCaretColor = true;
        Color caretColor = input_field.caretColor;
        caretColor.a = value;
        input_field.caretColor = caretColor;
    }
}
