using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StoryText: MonoBehaviour
{
    public Dictionary<string, Conversation> story = new Dictionary<string, Conversation>();
    protected Dictionary<string, GameObject> characterMapping = new Dictionary<string, GameObject>();
    public Dictionary<string, Coroutine> currentConversation = new Dictionary<string, Coroutine>();

    public TextAsset storyFile = null;
    private void Awake()
    {
        story = StoryFileParser.ParseStory(storyFile.text);
        /*
        //test code
#if DEBUG
        foreach (var k in story.Keys)
        {
            Conversation conver = story[k];
            Debug.Log("------------------");
            Debug.Log("Story Part " + k);
            foreach(var c in conver.contents)
            {
                Debug.Log("["+c.characterName+"] <"+c.commandType+"> "+c.content);
            }
            
        }
#endif*/

    }

    public void PlayStory(string name)
    {
        Conversation c = story[name];
        Debug.Log("Start conversation " + name);
        currentConversation[name] = StartCoroutine(PlayConversation(c));
    }

    private GameObject GetCharacter(string name)
    {
        if (characterMapping.ContainsKey(name))
        {
            return characterMapping[name];
        }
        else
        {
            GameObject c = GameObject.Find(name);
            if (c != null)
            {
                characterMapping.Add(name, c);
                return c;
            }
            else
            {
                return null;
            }
        }
    }

    IEnumerator PlayConversation(Conversation c)
    {
        foreach(var command in c.contents){
            switch (command.commandType)
            {
                case Conversation.CommandType.Speek:
                    GameObject g = GetCharacter(command.characterName);
                    CharacterElement characterElement = g.GetComponent<CharacterElement>();
                    characterElement.Say(command.content);
                    while (characterElement.dialogBubble.IsPlaying)
                    {
                        yield return new WaitForSeconds(characterElement.dialogBubble.LeftPlayingTime*0.51f);
                    }
                break;
                case Conversation.CommandType.ExecuteFunction:
                    // for future use
                    break;
            }
        }
        yield break;
    }


}