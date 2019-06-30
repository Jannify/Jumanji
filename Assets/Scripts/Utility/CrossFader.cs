using System.Collections.Generic;
using UnityEngine;

public class CrossFader : MonoBehaviour
{
    public static CrossFader Main;

    [SerializeField]
    private float FadePerSecond = 1;
    private Dictionary<CanvasGroup, int> fadingObjects = new Dictionary<CanvasGroup, int>();

    private void Awake()
    {
        Main = this;
    }

    public static void crossFadeCanvasGroup(GameObject gameObject, int direction)
    {
        if (gameObject?.GetComponent<CanvasGroup>() != null)
        {
            CanvasGroup canvas = gameObject.GetComponent<CanvasGroup>();
            if (canvas != null)
            {
                canvas.alpha = direction == 1 ? 0 : 1;
                if (!Main.fadingObjects.ContainsKey(canvas)) Main.fadingObjects.Add(canvas, direction);
                else Main.fadingObjects[canvas] = direction;
            }
        }
        else Debug.LogError(gameObject + "  has no CanvasGroup");
    }

    public static void crossFadeCanvasGroup(CanvasGroup canvasGroup, int direction)
    {
        Main.fadingObjects.Add(canvasGroup, direction);
    }

    private void Update()
    {
        if (fadingObjects.Count != 0)
        {
            List<CanvasGroup> toRemove = new List<CanvasGroup>();
            foreach (KeyValuePair<CanvasGroup, int> obj in fadingObjects)
            {
                if(obj.Key == null)
                {
                    toRemove.Add(obj.Key);
                }
                obj.Key.alpha += FadePerSecond * obj.Value * Time.deltaTime;
                if (obj.Value == 1 && obj.Key.alpha >= 1) toRemove.Add(obj.Key);
                else if (obj.Value == -1 && obj.Key.alpha <= 0) toRemove.Add(obj.Key);
            }
            foreach (CanvasGroup item in toRemove)
            {
                fadingObjects.Remove(item);
            }
        }
    }
}
