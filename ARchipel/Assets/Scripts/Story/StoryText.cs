using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StoryText: MonoBehaviour
{
    public Dictionary<string, Conversation> story = new Dictionary<string, Conversation>();
    public Dictionary<string, GameObject> characterMapping = new Dictionary<string, GameObject>();

    public TextAsset storyFile = null;
    private void Start()
    {
        story = StoryFileParser.ParseStory(storyFile.text);

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
#endif
    }

    private void Update()
    {
        
    }

}