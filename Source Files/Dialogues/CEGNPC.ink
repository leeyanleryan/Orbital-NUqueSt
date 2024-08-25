INCLUDE globals.ink
EXTERNAL QuestCompleted()
-> main

=== main ===
TeeHaw: Hmmm... How much ram will be needed to run this in my computer...
TeeHaw: Oh! Hi there!
* [What's up?]
    -> NormalConvo
    -> DONE
* [Do you have any quests?]
{(!CEGValidTime): -> NotValidTime |{CEGAllDone: -> TeeHawScrollConvo |{CEGQuestStarted:TeeHaw: I have already given you a quest. -> in_quest|{(!questCG1111ADone): -> CG1111AConvo |{(!questEG1311Done): -> EG1311Convo |{(!questCG2111ADone): -> CG2111AConvo}}}}}}-> DONE
* [Leave]
  TeeHaw: Goodbye.-> DONE
-> END

=== NormalConvo ===
TeeHaw: Nothing much! Just wondering what I should do for my next project!
TeeHaw: In case you've read my note in the desert cave, I cover the hole to seal the monsters away!
TeeHaw: It's very scary down there!
TeeHaw: However, if you accept my quest, I will remove the cover!
-> DONE

=== NotValidTime ===
TeeHaw: Thanks for the help! Maybe come another time.
(You can only accept one quest from each villager per day.)
(Help as many villagers as you can before the day ends!)
-> END

=== TeeHawScrollConvo ===
{(!CEGScrollConvo):
TeeHaw: My robot discovered something!!!
TeeHaw: I deployed it in the forest south of this village to investigate some loud noise!
TeeHaw: Then when my robot came back, it retrieved two scrolls!!
TeeHaw: But I was controlling it from my house, and when I went to take the scrolls, one of it was missing...
TeeHaw: Maybe the sensor had detected the same scroll twice, but regardless!
TeeHaw: This discovery was only possible because of you! So, I want you to have this scroll!!
TeeHaw: Check the chest inside my house for the scroll!
~CEGScrollConvo = true
-> END
}
TeeHaw: My robot is working really well!!
TeeHaw: I don't think I need to test any more features after this...
TeeHaw: Thank you for your help! 
TeeHaw: Maybe ask my partner Galileo, or somebody else for quests!
-> END

=== CG2111AConvo ===
{(!CEGQuestDone):
TeeHaw: OK!!! The final product is done!!!!
TeeHaw: This will be the final request >.< !!!
TeeHaw: The final test will be to check whether all the features of my robot are present!
TeeHaw: We will be running similar tests as the last time BUTTTTT this time you will not be able to see the maze!
TeeHaw: Instead, you will rely on a GPS!
TeeHaw: Don't worry! You got this!
TeeHaw: So do you think you can help me with the final test??
-> start_quest("CG2111A", "Test my final product at the third level in the cave of the desert west of this village!")
->END
}
~ QuestCompleted()
TeeHaw: Finally! My robot works!! Without you I don't think I would have done it easily!!
TeeHaw: With this robot I can defend the village against the monsters!!
TeeHaw: Maybe I can even use it to investigate about the new SU Monster I have been hearing....
TeeHaw: Anyway, Thanks for the help!
~questCG2111ADone = true
~CEGValidTime = false
~CEGAllDone = true
~CEGQuestDone = false
->END


=== EG1311Convo ===
{(!CEGQuestDone):
    TeeHaw: Thanks for the help with my prototype previously! Now I need to test it out further!
    TeeHaw: I need it to overcome an obstacle that I created in one of the levels of the cave in the desert west of this village!
    TeeHaw: It is some simple obstacle where the robot prototype has to go up and down a slope followed by throwing a rock at a target!
    -> start_quest("EG1311", "Use my prototype to overcome one of the obstacle in the cave of the desert west of village!")
    -> END
    }
    ~ QuestCompleted()
    TeeHaw: LEZGOOOOO!!!! It seems like my prototype is smurfing through these test cases!!
    TeeHaw: I think I am gonna build the final product and give it a final test next time!
    TeeHaw: Thanks for the help!!! Once I finish building the final product you can come and find me! Till next time!
     ~questEG1311Done = true
     ~CEGValidTime = false
     ~CEGQuestDone = false
->END


=== CG1111AConvo ===
{(!CEGQuestDone):
    TeeHaw: You must be the new person in town! My name is TeeHaw. I am a computer engineer! 
    TeeHaw: Based on your facial expression you don't seem to be surprised! That must mean there are computer engineers in your world!
    TeeHaw: Anyway, I have been working on a project recently and I need some help!
    TeeHaw: I need you to help me with testing a prototype of mine! I placed it in one of the level in the cave of the dessert!
    TeeHaw: Can you help me make sure you pass the test puzzles that I've made using my prototype?
    TeeHaw: Don't worry! It's a simple puzzle where you have to control the prototype robot and sense the correct colors on the walls!
    -> start_quest("CG1111A", "Test the prototype that I have placed in the cave in the desert west of village!")
    ->END
    }
    ~ QuestCompleted()
    TeeHaw: Amazing! My prototype seems to be working! Thanks for the help! Maybe I can use this prototype to defend against the monsters outside of our village!
     ~questCG1111ADone = true
     ~CEGValidTime = false
     ~CEGQuestDone = false
    -> END

=== in_quest ===
* [What am I supposed to do again?]
TeeHaw: {CEGQuestDesc}-> DONE
* [Leave]
  TeeHaw: Goodbye.-> DONE

-> END

=== start_quest(Name, Desc) ===
* [Yes]
  TeeHaw: Great!
  ~ CEGQuestName = Name
  ~ CEGQuestDesc = Desc
  ~ CEGQuestStarted = true
  -> DONE
* [No]
  TeeHaw: Sleep with one eye open :)-> DONE
-> END