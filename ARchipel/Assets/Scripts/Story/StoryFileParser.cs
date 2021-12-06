using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryFileParser
{
    /**
     * Input: text need to be processed
     * Output: a dictionary of conversations, variable[Conversation_id] => Conversation 
     * 
     * fast coded, not consider too much about grammar and tokenize.
    */
    public static Dictionary<string, Conversation> ParseStory(string s)
    {
        Dictionary<string, Conversation> result = new Dictionary<string, Conversation>();

        // first, process into tokens
        string[] lines = s.Split('\n');
        int lineIndex = 0;

        string currentBlockName = null;
        while (lineIndex < lines.Length)
        {
            string curLine = lines[lineIndex].Trim();
            if (curLine.Length > 0)
            {
                switch (curLine[0])
                {
                    case '@':
                        currentBlockName = curLine.Substring(1).TrimStart();
                        result[currentBlockName] = new Conversation();
                        break;
                    case '#':
                        //comment, ignore
                        break;
                    case '[':
                        //character begin
                        Conversation conversation = result[currentBlockName];
                        int endTokenIndex = curLine.IndexOf(']');
                        string characterName = curLine.Substring(1, endTokenIndex - 1);
                        conversation.AddSpeek(characterName, curLine.Substring(endTokenIndex));
                        bool flag = true;
                        while (lineIndex < lines.Length-1 && flag)
                        {
                            lineIndex++;
                            string curLine2 = lines[lineIndex].Trim();
                            if (curLine2.Length == 0)
                            {
                                continue;
                            }
                            switch (curLine2[0])
                            {
                                case '#':
                                    //comment, ignore
                                    break;
                                case '@':
                                    flag = false;
                                    lineIndex--;
                                    break;
                                case '[':
                                    flag = false;
                                    lineIndex--;
                                    break;
                                case ']':
                                    throw new System.Exception("Encounter wrong character [ at line " + lineIndex);
                                default:
                                    conversation.AddSpeek(characterName, curLine2);
                                    break;
                            }
                        }
                        break;
                    case '*':
                        // not implemented
                        break;
                    default:
                        break;

                }
            }
            lineIndex++;
        }

        return result;
    }

}