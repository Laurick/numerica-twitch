using System;
using System.Collections.Generic;
using TwitchChat;
using UnityEngine;

public class CustomChatter: IEquatable<CustomChatter>
{
    private Chatter chatter;

    public CustomChatter(Chatter chatter)
    {
        this.chatter = chatter;
    }

    public bool Equals(CustomChatter other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        Debug.Log($"compare - {chatter.login} <-> {other.chatter.login}");
        
        return Equals(chatter.login, other.chatter.login);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CustomChatter)obj);
    }

    public override int GetHashCode()
    {
        return (chatter != null ? chatter.GetHashCode() : 0);
    }
}
