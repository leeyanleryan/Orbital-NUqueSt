INCLUDE globals.ink
EXTERNAL QuestCompleted()

Me: This purple flower looks different.
Me: I don't think there's any other special purple flowers in the caves.
Me: I believe this is the one Eve is looking for.
However, you are not an expert on flowers, and cannot identify the exact species of this flora.

~ HSI1000FlowersFound += 1

{(HSI1000FlowersFound == 3): Me: Seems like that's all of them. Better report back to Eve.|Me: Better find more flowers.}