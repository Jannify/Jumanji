using TMPro;
using UnityEngine;

public class LockNumbers : baseLockMode
{
    private static LockNumbers Main;

    [SerializeField]
    private GameObject numberPrefab;

    private TextMeshProUGUI[] numbers;

    private int currentNumberSlot = 1;

    void Awake()
    {
        Main = this;
    }

    public override void setNextChar(string number)
    {
        if (Main.currentNumberSlot < Main.numbers.Length)
        {
            Main.numbers[Main.currentNumberSlot].SetText(number);
            Main.currentNumberSlot++;
        }
        else
        {
            Main.numbers[Main.currentNumberSlot].SetText(number);
            string code = "";
            foreach (TextMeshProUGUI num in Main.numbers)
            {
                code = code + num.text;
            }
            checkPassword(code);
        }
    }
}
