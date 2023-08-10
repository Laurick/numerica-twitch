using System;
using TwitchChat;

public class CustomChatter: IComparable<CustomChatter>
{
    private Chatter chatter;

    public CustomChatter(Chatter chatter)
    {
        this.chatter = chatter;
    }
    
    public int CompareTo(CustomChatter other)
    {
        if (other == null) return 1;
        
        return chatter.login.CompareTo(other.chatter.login);
    }
}
