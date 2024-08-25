INCLUDE globals.ink
EXTERNAL QuestCompleted()
-> main

=== main ===
{tutorialMayorSpoken: -> tutorialMayorOver | -> tutorialMayorProgress}

-> END

=== tutorialMayorOver ===
NUS Town Mayor: Hello. Much apologies, I am busy at the moment.

-> END

=== tutorialMayorProgress ===
???: Hey there. You're awake at last. How are you feeling?
Me: I'm feeling nauseous.
???: That's unfortunate. I hope you feel better soon.
???: Also, I have never seen you before.
NUS Town Mayor: We're in a town called NUS. I'm the mayor of this town.
NUS Town Mayor: May I know your name?
Me: I'm Noah.
NUS Town Mayor: Nice to meet you, Noah. 
NUS Town Mayor: If you have any questions, you can ask my secretary.
NUS Town Mayor: She's in the living room downstairs.
NUS Town Mayor: I have to return to my work now. Goodbye.

~ tutorialMayorSpoken = true
    
-> END

