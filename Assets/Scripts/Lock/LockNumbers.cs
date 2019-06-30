using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LockNumbers : baseLockMode
{
    public int maxNumber;

    [SerializeField]
    private GameObject numberPrefab = default;

    private List<Number> numbers = new List<Number>();


    public override void setNextChar(string number)
    {
        GameObject nextChar = Instantiate(numberPrefab, transform.GetChild(0));
        CrossFader.crossFadeCanvasGroup(nextChar, 1);
        numbers.Add(nextChar.GetComponent<Number>());
        numbers.Last().SetNumber(number);

        if (numbers.Count >= maxNumber)
        {
            string code = "";
            foreach (Number num in numbers)
            {
                code += num.number;
                CrossFader.crossFadeCanvasGroup(num.gameObject, -1);
                Destroy(num.gameObject, 1.0f);
            }
            if (!checkPassword(code))
            {
                Debug.LogWarning(code + " No key found.");
            }
            numbers.Clear();
        }
    }
}
