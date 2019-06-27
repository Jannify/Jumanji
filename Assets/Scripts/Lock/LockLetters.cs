using System;
using TMPro;
using UnityEngine;

public class LockLetters : baseLockMode
{
    [SerializeField]
    TMP_InputField inputField = default;

    public void openKeyboard()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NamePhonePad, false, false);
    }

    public void checkLetterPassword(string password)
    {
        if(!checkPassword(password))
        {
            Debug.Log("No key found.");
            inputField.text = "";
        }
    }
}
