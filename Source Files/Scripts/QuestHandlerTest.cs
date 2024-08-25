using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static QuestList;

public class QuestHandlerTest
{
    public QuestList questList;
    public DialogueManager dialogueManager;

    QuestSlot questSlotTest;
    [SetUp]
    public void SetUp()
    {
        questList = new QuestList(6);
        questSlotTest = questList.questSlots[0];
        questSlotTest.testing = true;
        questList.questSlots[0].testing = true;
    }
    // A Test behaves as an ordinary method
    [Test]
    public void QuestHandlerTestSimplePasses()
    {

        questList.Add("MA1508E", "Quest MA1512 Description");
        questList.questSlots[0].QuestHandler("MA1508E");
        Assert.AreEqual(20, questList.questSlots[0].gpaReward);        
               
        questSlotTest.QuestHandler("HSA1000");
        Assert.AreEqual(20, questSlotTest.gpaReward);
               
        questSlotTest.QuestHandler("GESS1001");
        Assert.AreEqual(20, questSlotTest.gpaReward);
               
        questSlotTest.QuestHandler("GEA1000");
        Assert.AreEqual(20, questSlotTest.gpaReward);
               
        questSlotTest.QuestHandler("PC1101");
        Assert.AreEqual(10, questSlotTest.gpaReward);
               
        questSlotTest.QuestHandler("PC1201");
        Assert.AreEqual(20, questSlotTest.gpaReward);
               
        questSlotTest.QuestHandler("CS1010");
        Assert.AreEqual(40, questSlotTest.gpaReward);
               
        questSlotTest.QuestHandler("CS1231");
        Assert.AreEqual(40, questSlotTest.gpaReward);
               
        questSlotTest.QuestHandler("CS2030");
        Assert.AreEqual(40, questSlotTest.gpaReward);
               
        questSlotTest.QuestHandler("CS2040");
        Assert.AreEqual(40, questSlotTest.gpaReward);
               
        questSlotTest.QuestHandler("CG1111A");
        Assert.AreEqual(30, questSlotTest.gpaReward);
               
        questSlotTest.QuestHandler("CG2111A");
        Assert.AreEqual(40, questSlotTest.gpaReward);
             
        questSlotTest.QuestHandler("EG1311");
        Assert.AreEqual(30, questSlotTest.gpaReward);
               
    }

    [Test]
    public void QuestListCountTest()
    {
        questList.questSlots.Clear();
        questList = new QuestList(4);
        Assert.AreEqual(4, questList.questSlots.Count);

        questList.questSlots.Clear();
        questList = new QuestList((int)8.5);
        Assert.AreEqual(8, questList.questSlots.Count);

        questList.questSlots.Clear();
        questList = new QuestList(0);
        Assert.AreEqual(0, questList.questSlots.Count);

        questList.questSlots.Clear();
        questList = new QuestList(2000);
        Assert.AreEqual(2000, questList.questSlots.Count);

    }

}
