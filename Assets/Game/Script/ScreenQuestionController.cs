using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScreenQuestionController : MonoBehaviour
{
    public static ScreenQuestionController instance;

    public Question currentQuestion;

    public Image background;
    public Text questionText;

    public Transform answerContainer;
    public Transform optionsContainer;
    public Transform emojisContainer;

    public Transform animationContainer;

    public AudioClip wonFeedBack;
    public AudioClip loseFeedBack;


    [Header("Questions Backgrounds")]

    public Sprite FOX;
    public Sprite FX;
    public Sprite FOX_LIFE;
    public Sprite FXM;
    public Sprite CINE_CANAL;
    public Sprite NATGEO;
    public Sprite NATGEOKIDS;
    public Sprite NATGEOWILD;

    public int maxIntents = 3;

    [Header("PopUp ")]
    public EasyTween popUp;
    public Text popUpText;
    [TextArea()]
    public string popUpBase;

    private int currentIntents = 0;

    private void Awake()
    {
        instance = this;
    }

    public void LoadQuestion(Question question)
    {
        currentIntents = 0;

        currentQuestion = question;

        Sprite backGround = FOX;

        switch (currentQuestion.canal)
        {
            case Canal.FOX:
                backGround = FOX;
                break;
            case Canal.FX:
                backGround = FX;
                break;
            case Canal.FOX_LIFE:
                backGround = FOX_LIFE;
                break;
            case Canal.FXM:
                backGround = FXM;
                break;
            case Canal.CINE_CANAL:
                backGround = CINE_CANAL;
                break;
            case Canal.NATGEO:
                backGround = NATGEO;
                break;
            case Canal.NATGEOKIDS:
                backGround = NATGEOKIDS;
                break;
            case Canal.NATGEOWILD:
                backGround = NATGEOWILD;
                break;
        }

        background.sprite = backGround;
        questionText.text = question.question;

        var emojis = emojisContainer.GetComponentsInChildren<Emoji>();

        var answersEmoji = emojis.Where(e => currentQuestion.answers.Contains(e)).ToList();

#if UNITY_EDITOR
        foreach (var item in answersEmoji)
        {
            item.GetComponent<Image>().color = Color.red;
        } 
#endif

        var optionsEmojis = emojis.Where(e => !answersEmoji.Contains(e)).ToList().Take(16 - answersEmoji.Count());

        optionsEmojis = optionsEmojis.Concat(answersEmoji);
        optionsEmojis = optionsEmojis.OrderBy(a => System.Guid.NewGuid());

        foreach (var _emoji in optionsEmojis)
        {
            _emoji.transform.SetParent(optionsContainer);
        }

    }

    public void Show()
    {
        GetComponent<EasyTween>().OpenCloseObjectAnimation();
    }

    public void Hide()
    {
        GetComponent<EasyTween>().OpenCloseObjectAnimation();
    }

    public void AnswerClicked(Emoji currentEmoji)
    {
        if (currentQuestion.answers.Contains(currentEmoji))
        {
            StartCoroutine(MoveEmojiToAnswers(currentEmoji));
        }
        else
        {
            GameManager.instance.BlockScreen();
            currentIntents++;
            transform.DOShakePosition(1, 15);

            if (currentIntents == maxIntents)
            {
                GameManager.instance.ShowLoseScreen();
            }
            else
            {
                ShowPopUpWrong();
            }
        }
    }

    void ShowPopUpWrong()
    {
        int remainIntents = maxIntents - currentIntents;
        popUpText.text = string.Format(popUpBase, remainIntents);
        popUp.OpenCloseObjectAnimation();
        AudioSource.PlayClipAtPoint(loseFeedBack, Camera.main.transform.position);
        Invoke("HidePopUp", 2);
    }

    void HidePopUp()
    {
        popUp.OpenCloseObjectAnimation();
    }

    IEnumerator MoveEmojiToAnswers(Emoji emoji)
    {
        GameManager.instance.BlockScreen();
        emoji.transform.SetParent(animationContainer);
        yield return new WaitForEndOfFrame();
        emoji.transform.DOMove(answerContainer.transform.position, 1f);
        yield return new WaitForSeconds(1f);
        emoji.transform.SetParent(answerContainer);

        yield return new WaitForSeconds(1f);


        if (answerContainer.childCount == currentQuestion.answers.Count())
        {
            //Show a Win Animation or something
            GameManager.instance.ShowWinScreen();
            AudioSource.PlayClipAtPoint(wonFeedBack, Camera.main.transform.position);
        }
    }

}
