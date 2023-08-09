using TwitchChat;
using UnityEngine.Events;

public abstract class Rule
{
    public int type;
    public string description;
    public UnityAction preConditions;
    public UnityAction postsConditions;

    protected Rule()
    {
   
    }

    protected Rule(UnityAction preConditions, UnityAction postsConditions)
    {
        this.preConditions = preConditions;
        this.postsConditions = postsConditions;
    }
    
    public void ExecutePreConditions()
    {
        preConditions?.Invoke();
    }
    
    public void ExecutePostConditions()
    {
        postsConditions?.Invoke();
    }
    
    public abstract bool isCorrectAnswer(AnswerInfo answerInfo);

    public virtual bool isRepeatable()
    {
        return false;
    }
}
