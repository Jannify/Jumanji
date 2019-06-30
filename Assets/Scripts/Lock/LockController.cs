using UnityEngine;

public class LockController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] lockModePrefabs = default;

    private GameObject currentMode;

    private void Start()
    {
        changeMode(GameController.passwordMode);
    }

    public void changeMode(bool numbers)
    {
        int i = numbers ? 0 : 1;
        if (currentMode)
        {
            CrossFader.crossFadeCanvasGroup(currentMode, -1);
            Destroy(currentMode, 1.5f);
        }
        currentMode = Instantiate(lockModePrefabs[i], transform);
        CrossFader.crossFadeCanvasGroup(currentMode, 1);
        GameController.passwordMode = numbers;
    }
}
