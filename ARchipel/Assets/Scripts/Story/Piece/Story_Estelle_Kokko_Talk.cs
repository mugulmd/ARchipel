using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Story_Estelle_Kokko_Talk : StoryPiece
{
    public EstelleLife estelle;
    public KokkoLife kokko;

    protected override void Start()
    {
        base.Start();

        playOnceOnly = true;
    }
    protected override bool JudgeCondition()
    {
        return kokko.IsOnThePlatform("Island Estelle")
            && estelle.IsOnThePlatform("Island Estelle")
            && !estelle.HasStoryTag("Meet Kokko")
            && !kokko.HasStoryTag("Meet Estelle");
    }

    protected override IEnumerator PlayContent()
    {
        kokko.isOccupiedByStory = true;
        estelle.isOccupiedByStory = true;
        kokko.LookAt(estelle.gameObject);
        estelle.LookAt(kokko.gameObject);
        kokko.storyTags.Add("Meet Estelle");
        estelle.storyTags.Add("Meet Kokko");
        yield return story_text.PlayStoryOnCoroutine("Estelle_Kokko_Talk");
        kokko.isOccupiedByStory = false;
        estelle.isOccupiedByStory = false;
    }
}
