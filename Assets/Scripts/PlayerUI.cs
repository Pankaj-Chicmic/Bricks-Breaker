using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inGameScoreText;
    [SerializeField] private TextMeshProUGUI inGameTotalPointsText;
    [SerializeField] private TextMeshProUGUI inGameSpecialDamageText;
    [SerializeField] private TextMeshProUGUI afterGameTotalPointsText;
    [SerializeField] private TextMeshProUGUI afterGameTotalScoreText;
    [SerializeField] private TextMeshProUGUI afterGamePercentageScoredText;
    [SerializeField] private TextMeshProUGUI afterGameTotalHitsText;
    [SerializeField] private TextMeshProUGUI afterGameTotalbricksText;
    [SerializeField] private TextMeshPro endPosBalls;
    [SerializeField] private TextMeshPro startPosBalls;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject fastForwardButton;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject startGamePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!endGamePanel.activeSelf)
            {
                Time.timeScale = 0;
                startGamePanel.SetActive(true);
            }
        }
    }
    public void ChangeActiveStateOfResetButton(bool state)
    {
        resetButton.SetActive(state);
        ChangeActiveStateOfFastForward(state);
    }
    public void ChangeActiveStateOfFastForward(bool state)
    {
        fastForwardButton.SetActive(state);
        if (!state)
        {
            Time.timeScale = 1;
        }
    }
    public void changeActiveStateOfSlider(bool state, bool speciallyPowered, int specialPowerNumber)
    {
        slider.gameObject.SetActive(state);
        if(!state)
        {
            inGameSpecialDamageText.text = "";
            return;
        }
        if (!speciallyPowered) inGameSpecialDamageText.text = "";
        else inGameSpecialDamageText.text = "2 X " + specialPowerNumber.ToString();
    }
    public Vector2 SliderValue()
    {
        return new Vector2(slider.value, slider.value > 0 ? 1 - slider.value : 1 + slider.value);
    }
    public void SetScore(int score)
    {
        inGameScoreText.text = "Points Scored " + score.ToString();
    }
    public void SetTotalPoints(int totalPoints)
    {
        inGameTotalPointsText.text = "Total Points " + totalPoints.ToString();
    }
    public void GameEnd(int score, int hits, int totalpoints, int totalBricks)
    {
        Time.timeScale = 0;
        endGamePanel.SetActive(true);
        endGamePanel.GetComponent<Animator>().SetBool("Play", true);
        afterGamePercentageScoredText.text = "Percentage Scored : " + ((float)score / (float)totalpoints * 100).ToString("#.##");
        afterGameTotalbricksText.text = "Total Bricks : " + totalBricks.ToString();
        afterGameTotalHitsText.text = "Total Hits : " + hits.ToString();
        afterGameTotalScoreText.text = "Points Scored : " + score.ToString();
        afterGameTotalPointsText.text = "Total points : " + totalpoints.ToString();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Ready()
    {
        Time.timeScale = 1;
        startGamePanel.SetActive(false);
    }
    public void FastForward()
    {
        Time.timeScale += 1;
    }
    public void SetStartPosBalls(Vector2 startPos,int balls)
    {
        startPos = new Vector2(startPos.x, startPos.y+0.3f);
        if (balls <= 0)
        {
            startPosBalls.text = "";
            return;
        }
        startPosBalls.transform.position = startPos;
        startPosBalls.text = "+ " + balls.ToString();
    }
    public void SetEndPosBalls(Vector2 endPos, int balls)
    {
        endPos = new Vector2(endPos.x, endPos.y+0.3f);
        if (balls <= 0)
        {
            endPosBalls.text = "";
            return;
        }
        endPosBalls.transform.position = endPos;
        endPosBalls.text = "+ " + balls.ToString();
    }
}
