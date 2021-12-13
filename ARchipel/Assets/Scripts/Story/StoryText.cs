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
    public Dictionary<string, Coroutine> currentConversation = new Dictionary<string, Coroutine>();
    protected GameData game_data;

    public TextAsset storyFile = null;
    private void Start()
    {
        story = StoryFileParser.ParseStory(storyFile.text);
        GameObject g = GameObject.Find("Game Manager");
        game_data = g.GetComponent<GameData>();
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

    // invoke this in the main thread
    public void PlayStory(string name)
    {
        Conversation c = story[name];
        Debug.Log("Start conversation " + name);
        currentConversation[name] = StartCoroutine(PlayConversation(c));
    }

    // this should be invoked by coroutines
    public IEnumerator PlayStoryOnCoroutine(string name)
    {
        Conversation c = story[name];
        Debug.Log("Start conversation Coroutine " + name);
        return PlayConversation(c);
    }

    private CharacterElement GetCharacter(string name)
    {
        return game_data.characterDict[name];
    }

    IEnumerator PlayConversation(Conversation c)
    {
        foreach(var command in c.contents){
            switch (command.commandType)
            {
                case Conversation.CommandType.Speek:
                    CharacterElement characterElement = GetCharacter(command.characterName);
                    characterElement.Say(command.content);
                    yield return new WaitUntil(() => !characterElement.dialogBubble.IsPlaying);
                break;
                case Conversation.CommandType.ExecuteFunction:
                    // for future use
                    break;
            }
        }
    }


}