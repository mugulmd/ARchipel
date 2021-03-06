using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class Conversation
{
    public List<Command> contents = new List<Command>();
    [System.Serializable]
    public enum CommandType
    {
        NoOperation=0,
        Speek,
        Decision, 
        ExecuteFunction // reflect to invoke the function
    }

    [System.Serializable]
    public class Command
    {
        public Command(string name, CommandType commandType, string content)
        {
            this.characterName = name;
            this.commandType = commandType;
            this.content = content;
        }
        public string characterName;
        public CommandType commandType;
        public string content;
    }

    public string conversation_yes, conversation_no;

    public void AddCommand(string name, CommandType commandType, string content)
    {
        contents.Add(new Command(name, commandType, content));
    }

    public void AddSpeek(string name, string content)
    {
        contents.Add(new Command(name, CommandType.Speek, content));
    }

    public void AddDecision(string content, string conv_yes, string conv_no)
    {
        contents.Add(new Command("decision", CommandType.Decision, content));
        conversation_yes = conv_yes;
        conversation_no = conv_no;
    }

    public void AddExecuteFunction(string name, string content)
    {
        contents.Add(new Command(name, CommandType.ExecuteFunction, content));
    }
}
