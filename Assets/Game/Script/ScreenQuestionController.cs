using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScreenQuestionController : MonoBehaviour {


    public Question currentQuestion;

    public Image background;
    public Text questionText;

    public Transform answerContainer;
    public Transform optionsContainer;
    public Transform emojisContainer;

    public void LoadQuestion (Question question)
    {
        currentQuestion = question;

        background.sprite = currentQuestion.backGround;
        questionText.text = question.question;

        var emojis = emojisContainer.GetComponentsInChildren<Emoji>();

        var answersEmoji = emojis.Where(e => currentQuestion.answers.Contains(e)).ToList();

        foreach (var emoji in answersEmoji)
        {
            emoji.transform.SetParent(optionsContainer);
        }

        var optionsEmojis = emojis.Where(e => !answersEmoji.Contains(e)).ToList();
        //TODO : SHuffle optionsEmoji List
        for (int i = 0; i < 16-answersEmoji.Count(); i++)
        {
            optionsEmojis.ElementAt(i).transform.SetParent(optionsContainer);
        }
    }

    public void Show ()
    {
        GetComponent<EasyTween>().OpenCloseObjectAnimation();
        
    }
}
