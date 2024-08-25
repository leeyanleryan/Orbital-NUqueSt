INCLUDE globals.ink
EXTERNAL QuestCompleted()
-> main

=== main ===
George: Hi! I'm George, Leo's older brother!
George: How can I help you?
    * [I want to buy something.]
        George: Sure. This is what we have in stock.
        -> DONE
    * [Do you have any quests?]
        {(!WeaponSmithValidTime): -> NotValidTime |{WeaponSmithAllDone: -> GeorgeScrollConvo|{WeaponSmithQuestStarted:George: I have already given you a quest. -> in_quest|{(!questMA1511Done): -> MA1511Convo|{(!questMA1512Done): -> MA1512Convo|{(!questMA1508EDone): ->MA1508EConvo}}}}}}
        -> DONE
    * [Leave]
        George: Goodbye.
        -> DONE

-> END

=== NotValidTime ===
George: Thanks for the help! Maybe come another time.
(You can only accept one quest from each villager per day.)
(Help as many villagers as you can before the day ends!)
-> END

=== GeorgeScrollConvo ===
{(!WeaponSmithScrollConvo):
George: As an extra reward for answering all those math questions, I'll be handing you a scroll.
George: This scroll was found by my brother deep in the caves.
George: It seems this scroll may be important, and I do not want to be tempted to use it as rough paper to write math stuff.
George: So, I'll be giving it to you.
George: The scroll is inside the chest in this house.
~WeaponSmithScrollConvo = true
-> END
}
George: It would seem that I cannot think of another math question for you.
George: Perhaps other villagers would need assistance!
-> END


=== in_quest ===
* [What am I supposed to do again?]
    George: {WeaponSmithQuestDesc}
    -> DONE
* [Leave]
    George: Goodbye.
    -> DONE

-> END

=== MA1511Convo ===
{(!WeaponSmithQuestDone):
George: This may seem odd, but I actually love math a lot.
George: One of my favourite topics is the infinite series.
George: It is fun when it comes to visualising something that never ends.
George: Would you like to solve an infinite series question?
-> start_quest("MA1511", "Solve the infinite series question upstairs!")
-> END
} 
~ QuestCompleted()
George: Nice one!
George: Come back tomorrow, and I'll give you another interesting question.
~questMA1511Done = true 
~WeaponSmithValidTime = false
~WeaponSmithQuestDone = false
->END

=== MA1512Convo === 
{(!WeaponSmithQuestDone):
George: Time for round 2!
George: The question is in the room on the left.
George: This time, the question will be on population modelling. Would you like to try it?
-> start_quest("MA1512", "Solve the population question upstairs!")
->END
}
~ QuestCompleted()
George: Amazing!
George: I still have one more question after this.
George: Come back next time and I'll ask you the question.
~questMA1512Done = true 
~WeaponSmithValidTime = false
~WeaponSmithQuestDone = false
->END

=== MA1508EConvo === 
{(!WeaponSmithQuestDone):
George: Did you know that the slimes and other monsters you see use Linear Algebra subconciously while they are fighting you?
George: It would seem unrelated, but it's actually true!
George: The way they move, the way they think, its all Linear Algebra!
George: In the desert cave, I have placed a linear algebra question on the wall. Would you like to solve it?
-> start_quest ("MA1508E", "Solve the linear algebra question in the desert cave!")
->END
}
~ QuestCompleted()
George: I see that you have solved it!
George: Who knew that linear algebra would actually turn out to be useful?
George: Anyway, if I ever come up with more questions, I will let you know!
~questMA1508EDone = true
~WeaponSmithValidTime = false
~WeaponSmithQuestDone = false
~WeaponSmithAllDone = true
->END


=== start_quest(Name, Desc) ===
* [Yes]
    George: Great!
    ~ WeaponSmithQuestName = Name
    ~ WeaponSmithQuestDesc = Desc
    ~ WeaponSmithQuestStarted = true
    ~ WeaponSmithQuestDone = false
    -> DONE
* [No]
    George: Ask me again when you're not busy!
    -> DONE
    
-> END

