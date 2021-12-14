using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Story_Cecil_Kokko_FirstMeet : StoryPiece
{
    CharacterElement cecil, kokko;
    IslandElement cecilIsland;
    protected override void Start()
    {
        base.Start();
        cecil = game_data.characterDict["Cecil"];
        kokko = game_data.characterDict["Kokko"];
        cecilIsland = game_data.islandDict["Island Cecil"];
        playOnceOnly = true;
    }
    protected override bool JudgeCondition()
    {
        return
            cecil.IsOnThePlatform("Island Cecil") &&
            kokko.IsOnThePlatform("Island Cecil") &&
            !cecil.HasStoryTag("Meet Kokko") &&
            !kokko.HasStoryTag("Meet Cecil");
    }

    protected override IEnumerator PlayContent()
    {
        cecil.isOccupiedByStory = true;
        kokko.isOccupiedByStory = true;
        kokko.LookAt(cecil.gameObject);
        cecil.LookAt(kokko.gameObject);
        yield return story_text.PlayStoryOnCoroutine("Cecil_Kokko_0");
        yield return new WaitUntil(() => cecilIsland.has_boat);
        yield return story_text.PlayStoryOnCoroutine("Cecil_Kokko_1");
        cecil.storyTags.Add("Meet Kokko");
        kokko.storyTags.Add("Meet Cecil");
        cecil.isOccupiedByStory = false;
        kokko.isOccupiedByStory = false;
    }
}
