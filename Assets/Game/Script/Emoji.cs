using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emoji : MonoBehaviour
{
    public void SetUp(Sprite sprite)
    {
        this.GetComponent<Image>().sprite = sprite;
    }

    public void OnEmojiPressed()
    {
        ScreenQuestionController.instance.AnswerClicked(this);
    }

}
