using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Story_Cecil_Kokko_Adventure : StoryPiece
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
        return kokko.ground.name == cecil.ground.name
            && kokko.HasStoryTag("Meet Cecil")
            && cecil.HasStoryTag("Meet Kokko")
            && cecil.HasStoryTag("No Aquaphobia");
    }

    protected override IEnumerator PlayContent()
    {
        cecil.isOccupiedByStory = true;
        kokko.isOccupiedByStory = true;
        kokko.LookAt(cecil.gameObject);
        cecil.LookAt(kokko.gameObject);
        yield return story_text.PlayStoryOnCoroutine("Cecil_Kokko_Adventure");
        cecil.isOccupiedByStory = false;
        kokko.isOccupiedByStory = false;
    }
}
