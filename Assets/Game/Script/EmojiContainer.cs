using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiContainer : MonoBehaviour {

    public List<Sprite> sprites;
    public GameObject emojiBase;

	public void CreateEmojisButtons ()
    {
        foreach (var item in sprites)
        {
            GameObject newEmoji = Instantiate(emojiBase, transform);
            newEmoji.GetComponent<Emoji>().SetUp(item);
            newEmoji.name = "Emoji " + newEmoji.transform.GetSiblingIndex();
        }
    }

}
