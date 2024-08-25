INCLUDE globals.ink
EXTERNAL QuestCompleted()
-> main

=== main ===
Eve: Hi there! What can I get for you?
    * [I want to buy something.]
        Eve: Sure. This is what we have in stock.
        -> DONE
    * [Do you have any quests?]
        {(!EveShopValidTime): -> NotValidTime |{EveShopAllDone: -> EveScrollConvo|{EveShopQuestStarted:Eve: I have already given you a quest. -> in_quest|{(!questDTK1234Done): -> DTK1234Convo|{(!questHSI1000Done): -> HSI1000Convo|{(!questHSS1000Done): ->HSS1000Convo}}}}}}-> DONE
        -> DONE
    * [Leave]
        Eve: Goodbye.
        -> DONE

-> END

=== NotValidTime ===
Eve: Thanks for the help! Maybe come another time.
(You can only accept one quest from each villager per day.)
(Help as many villagers as you can before the day ends!)
-> END

=== EveScrollConvo ===
{(!EveShopScrollConvo):
Eve: This isn't really a quest, but I found a scroll while I was in the forest looking for inspiration.
Eve: I don't have a use for it so I'd like to give it to you.
Eve: Maybe you could find a use for it somewhere?
Eve: The scroll is inside the chest in my house.
~EveShopScrollConvo = true
-> END
}
Eve: You've been really helpful to me!
Eve: I don't think I will need help for a while now.
Eve: You should ask someone else for a quest!
-> END

=== HSS1000Convo ===
{(!EveShopQuestDone):
    Eve: Other than expressing myself through art and craft, I also love writing poems.
    Eve: I have some really bad poems stashed up in my drawers.
    Eve: However, I think I am improving!
    Eve: I think my latest poem is really good!
    Eve: Could you read my poem in my house and tell me your opinion?
    -> start_quest("HSS1000", "Read my poem in my house and let me know what you think!")
    ->END
    }
    ~ QuestCompleted()
    Eve: Thank you for your feedback!
    Eve: I will continue writing my poems while on shopkeeper duty.
     ~questHSS1000Done = true
     ~EveShopValidTime = false
     ~EveShopAllDone = true
     ~EveShopQuestDone = false
    -> END

=== HSI1000Convo ===
{(!EveShopQuestDone):
    Eve: Drawing art is no easy feat.
    Eve: I'm usually out in nature looking for a beautiful landscape to draw.
    Eve: I read a book about flora and now I'm curious.
    Eve: Apparently there is a flower unique to every biome.
    Eve: One for the desert, one in the caves, and one in the forest.
    Eve: Could you help me find the flowers?
    -> start_quest("HSI1000", "Help me look for a unique flower in the desert, the caves and the forest!")
    ->END
    }
    ~ QuestCompleted()
    Eve: Wow! The book appears to be right!
    Eve: I think I know what to draw next...
    Eve: Thank you very much!
     ~questHSI1000Done = true
     ~EveShopValidTime = false
     ~EveShopQuestDone = false
    -> END

=== DTK1234Convo ===
{(!EveShopQuestDone):
    Eve: I have not told you this yet, but I am an artist!
    Eve: I love drawing art and making craft.
    Eve: Right now, there aren't many customers coming in so I'm bored.
    Eve: There are some leather pieces in my house.
    Eve: My house has a paintbrush logo on top of the door.
    Eve: Will you help me bring my leather pieces?
    -> start_quest("DTK1234", "Bring me my leather pieces from my house.")
    ->END
    }
    ~ QuestCompleted()
    Eve: Thank you for bringing them!
    Eve: In case you're wondering, I wanted to make a wallet.
     ~questDTK1234Done = true
     ~EveShopValidTime = false
     ~EveShopQuestDone = false
    -> END

=== in_quest ===
* [What am I supposed to do again?]
Eve: {EveShopQuestDesc}-> DONE
* [Leave]
  Eve: Goodbye.-> DONE

-> END

=== start_quest(Name, Desc) ===
* [Yes]
  Eve: Great!
  ~ EveShopQuestName = Name
  ~ EveShopQuestDesc = Desc
  ~ EveShopQuestStarted = true
  -> DONE
* [No]
  Eve: Oh... okay. -> DONE
-> END