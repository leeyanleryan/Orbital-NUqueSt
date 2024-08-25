INCLUDE globals.ink

EXTERNAL QuestCompleted()
-> main

=== main ===
Galileo: Hmmm this light seems strange, I wonder what it will do in this...
Galileo: Oh hey there!
* [What's up?]
     -> NormalConvo
     -> DONE
* [Do you have any quests?]
      {(!PhysicistValidTime): -> NotValidTime |{PhysicistAllDone: -> GalileoScrollConvo |{PhysicistQuestStarted:Galileo: I have already given you a quest. -> in_quest|{(!questPC1101Done): -> PC1101Convo |{(!questPC1201Done): -> PC1201Convo}}}}}-> DONE
* [Leave]
      Galileo: Goodbye.-> DONE
-> END

=== NormalConvo ===
Galileo: I'm on the verge of discovering something spectacular!
Galileo: Just wait till you see!
Galileo: I will prove Aristotle wrong!!!
-> DONE

=== NotValidTime ===
Galileo: Thanks for the help! Maybe come another time.
(You can only accept one quest from each villager per day.)
(Help as many villagers as you can before the day ends!)
-> END

=== GalileoScrollConvo ===
{(!PhysicistScrollConvo):
Galileo: There you are!
Galileo: I have been standing under every tree to confirm my theory on gravity, when suddenly, a scroll fell from the tree!
Galileo: Can you believe it?! A scroll!
Galileo: Who knows what knowledge this scroll could have!
Galileo: I'm leaving this scroll in your hands.
Galileo: Take it, and figure out what to do with it!
Galileo: The mysterious scroll is inside the brown chest in my house.
~PhysicistScrollConvo = true
-> END
}
Galileo: Alas, I have not any more quests for you.
Galileo: Perhaps you should ask my partner TeeHaw, or someone else!
-> END

=== PC1201Convo ===
{(!PhysicistQuestDone):
    Galileo: Heyyy. Thanks for helping me discover the 4 moons around POOPiter! 
    Galileo: Now, I got a new quest for you!
    Galileo: Do you think you can help me with it?
    Galileo: Ever since the discovery of the 4 moons I have been wondering about GRAVITY!
    Galileo: But, I feel like I should familiarise with the gravity on our planet here first!
    Galileo: Since you came recently, the gravity here must feel a bit off for you! 
    Galileo: Do you think you could help me with measuring the acceleration due to gravity here? 
    Galileo: You can do that by observing the rate at which the apple falls from one of the trees in the forest west of this village!
    Galileo: So...what do you say? You think you can help me with it?
    -> start_quest("PC1201", "Find the acceleration due to gravity on this planet by observing one of the trees in the West Forest!")
    -> END
    }
    ~ QuestCompleted()
    Galileo: WOW! It seems like you have knack for this! 
    Galileo: I shall give you the name Sir Issac Newton!
     ~questPC1201Done = true
     ~PhysicistValidTime = false
     ~PhysicistQuestDone = false
     ~PhysicistAllDone = true
->END


=== PC1101Convo ===
{(!PhysicistQuestDone):
    Galileo: You must be the new person in town! My name is Galileo!
    Galileo: You see as a physicist myself, I am restless when it comes to discovering new things!
    Galileo: Lately, I have been seeing dim lights around one of the planet in our galaxy of this world!
    Galileo: I have a good feeling this may be something BIG. Maybe it could be moons, in fact FOUR MOONS!!!! 
    Galileo: Sadly, my eyes are failing me this past few days as I have been looking at too many papers... As a result, I've gotten sore eyes...
    Galileo: So what do you say? 
    Galileo: Do you think you can help me go take a look into my telescope outside my house?
    Galileo: Help me find out if there are in fact four moons beside the planet Poopiter!
    -> start_quest("PC1101", "Find out if there are moons around the planet Poopiter!")
    ->END
    }
    ~ QuestCompleted()
    Galileo: Marvelous!! I was right! 
    Galileo: There ARE four moons around the planet POOPITER.
    Galileo: What should I name them...
    Galileo: Anyway, much appreciated for the help!
     ~questPC1101Done = true
     ~PhysicistValidTime = false
     ~PhysicistQuestDone = false
    -> END

=== in_quest ===
* [What am I supposed to do again?]
Galileo: {PhysicistQuestDesc}-> DONE
* [Leave]
  Galileo: Goodbye.-> DONE

-> END

=== start_quest(Name, Desc) ===
* [Yes]
  Galileo: Great!
  ~ PhysicistQuestName = Name
  ~ PhysicistQuestDesc = Desc
  ~ PhysicistQuestStarted = true
  -> DONE
* [No]
  Galileo: Disappointing.-> DONE
-> END
message.txt
5 KB