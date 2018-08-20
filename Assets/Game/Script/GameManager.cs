using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public EasyTween mainScreen;
    public EasyTween screenStart;
    public EasyTween screenInstructions;
    public ScreenQuestionController questionScreen;
    public EasyTween screenCongratulation;
    public EasyTween screenTryAgain;
    public EasyTween prizeInformation;

    [Header("Question List")]
    public List<Question> questions = new List<Question>();

    [Header("Blocker Panel Manager")]
    public GameObject blockPanel;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        instance = this;
    }

    public void OnMainScreenTouched()
    {
        blockPanel.SetActive(true);
        mainScreen.OpenCloseObjectAnimation();
        screenStart.OpenCloseObjectAnimation();
        Invoke("TurnOffBlockPanel", 1.5f);
    }

    public void OnStartScreenTouched()
    {
        blockPanel.SetActive(true);
        screenStart.OpenCloseObjectAnimation();
        screenInstructions.OpenCloseObjectAnimation();
        Invoke("TurnOffBlockPanel", 1.5f);
    }

    public void OnSTartQuestions()
    {
        blockPanel.SetActive(true);
        screenInstructions.OpenCloseObjectAnimation();
        questionScreen.Show();
        List<string> usedQuestions = PlayerPrefs.GetString("usedQ").Split('|').ToList();
        usedQuestions.Add(questions[0].question);
        if (usedQuestions.Count < questions.Count)
        {
            var unUsedQuestions = questions.Where(q => !usedQuestions.Contains(q.question)).ToList();
            Debug.Log(unUsedQuestions.Count);

            var questionRan = unUsedQuestions[Random.Range(0, unUsedQuestions.Count())];
            questionScreen.LoadQuestion(questionRan);
            usedQuestions.Add(questionRan.question);

            string _usedQuestions = "";
            foreach (var item in usedQuestions)
            {
                _usedQuestions += questionRan.question +"|";
            }

            PlayerPrefs.SetString("usedQ", _usedQuestions);
            Debug.Log(PlayerPrefs.GetString("usedQ"));

        }
        else
        {
            PlayerPrefs.SetString("usedQ", questions[0].question +"|" );
            questionScreen.LoadQuestion(questions[0]);
        }

        Invoke("TurnOffBlockPanel", 1.5f);
    }

    public void ShowLoseScreen()
    {
        BlockScreen();
        screenTryAgain.OpenCloseObjectAnimation();
        questionScreen.Hide();
        Invoke("RestartGame", 10);
    }

    public void ShowWinScreen()
    {
        BlockScreen();
        screenCongratulation.OpenCloseObjectAnimation();
        questionScreen.Hide();
        Invoke("ShowPrizeInformation", 4);
    }

    public void ShowPrizeInformation()
    {
        BlockScreen();
        prizeInformation.OpenCloseObjectAnimation();
        ScreenPrizeController.instance.LoadScreen();
        screenCongratulation.OpenCloseObjectAnimation();

        Invoke("RestartGame", 10);
    }

    public void BlockScreen()
    {
        blockPanel.SetActive(true);
        Invoke("TurnOffBlockPanel", 1.5f);
    }

    private void TurnOffBlockPanel()
    {
        blockPanel.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
}


[System.Serializable]
public struct Question
{
    public Canal canal;
    public string question;
    public List<Emoji> answers;
}

public enum Canal
{
    FOX,
    FX,
    FOX_LIFE,
    FXM,
    CINE_CANAL,
    NATGEO,
    NATGEOKIDS,
    NATGEOWILD
}





