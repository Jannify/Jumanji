using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LockNumbers : baseLockMode
{
    public int maxNumber;

    [SerializeField]
    private GameObject numberPrefab = default;

    private List<TextMeshProUGUI> numbers = new List<TextMeshProUGUI>();

    public override void setNextChar(string number)
    {
        numbers.Add(Instantiate(numberPrefab, transform.GetChild(0)).GetComponentInChildren<TextMeshProUGUI>());
        numbers.Last().SetText(number);

        if (numbers.Count >= maxNumber)
        {
            string code = "";
            foreach (TextMeshProUGUI num in numbers)
            {
                code = code + num.text;
                Destroy(num.transform.parent.gameObject);   
            }
            numbers.Clear();
            checkPassword(code);
        }
    }
}
