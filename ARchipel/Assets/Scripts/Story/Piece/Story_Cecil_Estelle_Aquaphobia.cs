using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Story_Cecil_Estelle_Aquaphobia : StoryPiece
{
    public CecilLife cecil;
    public EstelleLife estelle;
    
    protected override void Start()
    {
        base.Start();
        
        playOnceOnly = true;
    }
    protected override bool JudgeCondition()
    {
        return estelle.IsOnThePlatform("Island Cecil");
    }

    protected override IEnumerator PlayContent()
    {
        cecil.isOccupiedByStory = true;
        estelle.isOccupiedByStory = true;
        yield return story_text.PlayStoryOnCoroutine("Estelle_Cecil_Aquaphobia");
        if (game_data.decision_ctrl.chose_yes)
        {
            cecil.OvercomeAquaphobia();
        }
        cecil.isOccupiedByStory = false;
        estelle.isOccupiedByStory = false;
    }
}
