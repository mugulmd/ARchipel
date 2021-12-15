using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Story_Cecil_Kokko_FirstMeet : StoryPiece
{
    public CecilLife cecil;
    public KokkoLife kokko;

    protected override void Start()
    {
        base.Start();

        playOnceOnly = true;
    }
    protected override bool JudgeCondition()
    {
        return kokko.IsOnThePlatform("Island Cecil")
            && cecil.IsOnThePlatform("Island Cecil")
            && !kokko.HasStoryTag("Meet Cecil")
            && !cecil.HasStoryTag("Meet Kokko");
    }

    protected override IEnumerator PlayContent()
    {
        cecil.isOccupiedByStory = true;
        kokko.isOccupiedByStory = true;
        kokko.LookAt(cecil.gameObject);
        cecil.LookAt(kokko.gameObject);
        cecil.storyTags.Add("Meet Kokko");
        kokko.storyTags.Add("Meet Cecil");
        yield return story_text.PlayStoryOnCoroutine("Cecil_Kokko_FirstMeet");
        if (game_data.decision_ctrl.chose_yes)
        {
            kokko.AddStoryTag("Know about floating island");
        }
        cecil.isOccupiedByStory = false;
        kokko.isOccupiedByStory = false;
    }
}
