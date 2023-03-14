using UnityEngine.Events;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int PretzelScore;
    void Start()
    {
        PretzelScore = PlayerPrefs.GetInt("PretzelScore");
        UpdateUI();
    }

    public void AddPretzelScore()
    {
        PretzelScore++;
        PlayerPrefs.SetInt("PretzelScore", PretzelScore);
    }

    public void ResetPretzelScore()
    {
        PretzelScore = 0;
        PlayerPrefs.SetInt("PretzelScore", PretzelScore);
    }

    public void UpdateUI()
    {
        GameObject.Find("PretzelScore").GetComponent<TMPro.TextMeshProUGUI>().text = PretzelScore.ToString();
    }
}
