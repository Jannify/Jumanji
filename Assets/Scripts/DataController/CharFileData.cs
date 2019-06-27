using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharFileData
{
    public static CharFileData Main;

    [SerializeField]
    public List<string> originalText = new List<string>();
    [SerializeField]
    public Dictionary<string, string> keyText = new Dictionary<string, string>();

    public CharFileData()
    {
        Main = this;
    }
}
