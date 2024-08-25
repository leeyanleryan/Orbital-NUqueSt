INCLUDE globals.ink
EXTERNAL QuestCompleted()
-> main

=== main ===
Sam: Hi! I'm Sam, I am a fan of PUZZLES!
Sam: How can I help you?
    * [What's up?]
        -> NormalConvo
        -> DONE
    * [Do you have any quests?]
        {(!NerdValidTime): -> NotValidTime |{NerdAllDone: -> SamScrollConvo|{NerdQuestStarted:Sam: I have already given you a quest. -> in_quest|{(!questCS1010Done): -> CS1010Convo|{(!questCS1231Done): -> CS1231Convo|{(!questCS2030Done) : ->CS2030Convo |{(!questCS2040Done): ->CS2040Convo}}}}}}}
        -> DONE
    * [Leave]
        Sam: Goodbye.
        -> DONE

-> END

=== NormalConvo ===
Sam: Nothing much!
Sam: I'm just reading books on coding.
Sam: If you're wondering about why I cover the hole, it'z cuz there's many monsters down there!
Sam: Only if you accept my quest, then I'll remove the cover!
-> DONE

=== NotValidTime ===
Sam: Thanks for the help! Maybe come another time.
(You can only accept one quest from each villager per day.)
(Help as many villagers as you can before the day ends!)
-> END

=== SamScrollConvo ===
{(!NerdScrollConvo):
Sam: Wazzzzup!!!
Sam: I was quite sad when there were no more puzzles left in the forest cave.
Sam: However, when I went to the back to the forest to search for puzzles, I came across a scroll left inside a robot!
Sam: It was like a hidden treasure!
Sam: Anyway, I'm not too interested in this hardware stuff.
Sam: So I'm gonna give this scroll to you.
Sam: I carefully laid the scroll inside the chest in my house!
~NerdScrollConvo = true
-> END
}
Sam: Sorry man! I cannot find anymore coding puzzles to do!
Sam: Maybe I should start making some of my own.
Sam: Anyway, you can go help someone else!
-> END


=== in_quest ===
* [What am I supposed to do again?]
    Sam: {NerdQuestDesc}
    -> DONE
* [Leave]
    Sam: Goodbye.
    -> DONE

-> END

=== CS2040Convo ===
{(!NerdQuestDone):
Sam: HELLLLLLLLLLLO. The maze has been so fun to explore after you helped me solve it.
Sam: After the puzzle I discovered something new! 
Sam: There seems to be a whole new floor in the cave with weird moveable rocks. 
Sam: I suspect you have to form some kind of configuration. If I remember correctly, it was labelled as N-queens or something
Sam: Do you think you can take a look for me?
-> start_quest("CS2040", "Go to fifth floor of cave and solve the N-queens puzzle!")
->END
}
~ QuestCompleted()
Sam: WOW. The puzzle was the hardest one I have seen and you were able to solve it!. You really are amazing!
Sam: I should tell other villagers about how you are a great puzzle solver! 
Sam: Maybe the mystery of the S/U Monster will finally be solved!
~questCS2040Done = true
~NerdValidTime = false
~NerdQuestDone = false
~NerdAllDone = true
->END


=== CS2030Convo ===
{(!NerdQuestDone):
Sam: Hey it's you again, my fav fellow quest enthusiast <3! 
Sam: Yesterday, I was at on the fourth level of the cave looking for clues for more puzzles!
Sam: I was using my trusty magnifying glass and I discovered a strange house!
Sam: This house had strange notes on the ground and I couldn't read it...
-> start_quest("CS2030", "Help me solve the puzzle in the fourth floor of the forest cave!")
-> END
} 
~ QuestCompleted()
Sam: SHEEEEESH how did you solve the puzzle?? 
Sam: I was completely stunned when I read the note. Not like I can read anyway!
Sam: Anyway, thanks for solving the puzzle! Now, we can finally move on to the next depth!

~questCS2030Done = true 
~NerdValidTime = false
~NerdQuestDone = false
->END

=== CS1231Convo === 
{(!NerdQuestDone):
Sam: Bruhhhhhhhhhhhhhhhhhhhh. You did an amazing job with the puzzle previously, mr yi long ma!
Sam: Anyway, theres a puzzle that I have so much trouble solving. It is such a brain teaser!
Sam: I will need you to go to the third level of the cave and solve a puzzle for me!
Sam: Do you think you can help me with it?
-> start_quest("CS1231", "Go to third floor of the forest cave and solve the puzzle!")
->END
} 
~ QuestCompleted()
Sam: WOW! You breeze through those logic question like nothing!     
Sam: Thanks for the help! My brain is a bit too fuzzy for this... @_@
~questCS1231Done = true 
~NerdValidTime = false
~NerdQuestDone = false
->END

=== CS1010Convo === 
{(!NerdQuestDone):
Sam: Hey You must be the new kid in town! Have you explore the cave south of the village?
Sam: There seems to be a lot of puzzles inside it. 
Sam: Lately, I have trouble solving a puzzle. 
Sam: It's something to do with some lever mechanics that works with something to do with AND XOR Gates and For LOOPS... 
Sam: Do you think you can help me solve it?
-> start_quest("CS1010", "Help me solve the puzzle in the second floor of the cave down south!")
->END
} #Quest completed response
~ QuestCompleted()
Sam: Damnnn... For a first timer, you are pretty good at solving puzzles! 
Sam: Thanks for the help! If I discover more puzzles I will let you know!
~questCS1010Done = true
~NerdValidTime = false
~NerdQuestDone = false
->END


=== start_quest(Name, Desc) ===
* [Yes]
    Sam: Great!
    ~ NerdQuestName = Name
    ~ NerdQuestDesc = Desc
    ~ NerdQuestStarted = true
    ~ NerdQuestDone = false
    -> DONE
* [No]
    Sam: Ok.
    -> DONE
    
-> END