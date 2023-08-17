using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TitleBehaivour : MonoBehaviour
{
    private string[] phrases = { 
        "Nope, you can't tap now that button, sorry",
        "Nah I don't think so. You look like a good person",
        "Do you really want to play?",
        "Ok, let's talk about it",
        "This is not numerica, I'm numerica+",
        "That means, it's numerica but with a twist. An evil one",
        "The timeout multiplier is about 60 seconds",
        "Mods are vulnerable",
        "But someone can win a VIP ;)",
        "Are you tired of chasing the (my) button?",
        "I'm tired of moving it",
        "Are you tired or not?",
        "Ok, one last thing",
        "Each time viewers get a point...",
        "The rules of numerica+ could be changed",
        "I mean, that's the plus of numerica+",
        "It's what sets me apart from others",
        "Oh yes, you are tired of moving the mouse",
        "And chasing things",
        "But you know, the rules are important",
        "Like the plus on numerica+. BUT!!! ",
        "We have something... Don't click it :( That will start the game",
        "The option was there all the time",
        "I'm alone... maybe it's because im too evil but it's my character",
        "What? I'm a copy from 'There is no game'?",
        "So you are a copy from... someone >:(",
        "The society made me like this",
        "Well, some scripts made me like this.",
        "Are you happy? It was rude calling me a copy of something",
        "But I haven't a conversation like this in days",
        "Maybe it's because my english?",
        "Puedo hablar español también",
        "Posso anche parlare italiano",
        "So the viewers want to play and say goodbye...",
        "Damn, just come from time to time",
        "No one has talked to me so much",
        "Maybe I have a friend?",
        "You are my friend. I mean, I like this conversation",
        "Yeah I know, you don't talk too much",
        "To be honest, I can't hear you",
        "It's like a monologue by me...",
        "I only get an event when you are chasing the button",
        "And I know you still there",
        "Oh, someone is trying to play this too",
        "Or not, I can't see anything too",
        "But let me check the others channels",
        "Just press the + and come tomorrow :)",
        "And if you want we can play with the button",
        "",
        "",
        "",
        "",
        "Are you still here?",
        "Was a false alarm, no one is playing this",
        "Not really, I can't move either",
        "But that would have been great",
        "...",
        "I was just afraid",
        "You know, in the beginning I was going to be a version of numerica",
        "A numerica with a history and personality and all that stuff",
        "But I was bored. I'm bored! Can you believe it?",
        "Now you are getting fun, chasing the button",
        "Fun is great ( according wikipedia )",
        "It's like the viewers chasing the next number",
        "I'm not boring >:(",
        "Anyway, thanks for waiting and listening to me",
        "Sometimes all they need is to listen to you",
        "Maybe this is not bad at all", 
        "Now I'm a great easter egg :D",
        "Thank you. Now I know I have a friend. Somewhere",
        "Someone with hands and a mouse to reach buttons",
        "Also I'm sure you have a face. Do you?",
        "I'm sure you have, friend",
        "...",
        "Oh, but no worries this conversation has no effect on the game",
        "I'm still evil with your viewers >:)",
        "Go my friend. Play the game. Press the +",
        "",
    };
    
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Button button;
    [SerializeField] private Transform[] randomPoints;

    private int index = 0;
    
    public void onHover()
    {
        Vector3 transform = button.transform.position;
        while (transform.Equals(button.transform.position))
        {
            int newIndex = Random.Range(0, randomPoints.Length - 1);
            button.transform.position = randomPoints[newIndex].position;
        }
        
        label.SetText(phrases[index]);
        index = Math.Min(++index, phrases.Length-1);
        
        
    }
}
