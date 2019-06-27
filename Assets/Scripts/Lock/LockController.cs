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
        Destroy(currentMode);
        currentMode = Instantiate(lockModePrefabs[i], transform);
        GameController.passwordMode = numbers;
    }
}
