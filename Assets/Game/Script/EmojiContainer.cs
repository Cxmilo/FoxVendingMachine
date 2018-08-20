using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EmojiContainer : MonoBehaviour
{

    public List<Sprite> sprites;
    public List<Sprite> emojisPng;
    public GameObject emojiBase;

    public void CreateEmojisButtons()
    {
        foreach (var item in sprites)
        {
            GameObject newEmoji = Instantiate(emojiBase, transform);
            newEmoji.GetComponent<Emoji>().SetUp(item);
            newEmoji.name = "Emoji " + newEmoji.transform.GetSiblingIndex();
        }


    }

    public void Transfer()
    {
        var emojis = GetComponentsInChildren<Image>();
        foreach (var item in emojis)
        {
            Sprite nEmoji = emojisPng.FirstOrDefault(e => e.name == item.sprite.name);
            if (nEmoji)
            {
                item.sprite = nEmoji;
            }
            else
            {
                Debug.LogError("A error");
            }
        }
    }

}
