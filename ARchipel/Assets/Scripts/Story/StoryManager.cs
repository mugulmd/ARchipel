using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 We play at most one story, paralleled stories are not supported currently.
 */
public class StoryManager : MonoBehaviour
{
    public List<StoryPiece> storyPieces;
    public GameObject storyPieceObject; // an object contain storypieces
    bool isPlaying;
    private void Awake()
    {
        storyPieces = new List<StoryPiece>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (storyPieceObject != null)
        {
            storyPieces.AddRange(storyPieceObject.GetComponents<StoryPiece>());
        }
    }

    public void TryPlayAvaliableStory()
    {
        if (isPlaying)
        {
            return;
        }
        foreach (StoryPiece story in storyPieces)
        {
            if (story.CanPlay)
            {
                Debug.Log("Play story piece " + story.storyName);
                story.Play();
                isPlaying = true;
            }
        }
    }

    public void NotifyPlayIsEnded()
    {
        isPlaying = false;
        Debug.Log("End story piece");
    }

    // Update is called once per frame
    void Update()
    {
        TryPlayAvaliableStory();
    }

}
