using TwitchChat;

public class AnswerInfo
{
    public int answer;
    public int current;
    public Chatter chatter;
    public float time;

    public AnswerInfo(int answer, int current, Chatter chatter, float time)
    {
        this.answer = answer;
        this.current = current;
        this.chatter = chatter;
        this.time = time;
    }
}
