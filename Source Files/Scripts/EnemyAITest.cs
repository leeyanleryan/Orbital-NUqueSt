using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyAITest
{
    EnemyAI enemyAI;
    [SetUp]
    public void SetUp()
    {
        GameObject testGameObject = new GameObject();
        enemyAI = testGameObject.AddComponent<EnemyAI>();

    }
    // A Test behaves as an ordinary method
    [Test]
    public void FindRadiusTest()
    {
        double expectedRadius = 5;
        double actualRadius = enemyAI.FindRadius(4, 3);
        Assert.AreEqual(expectedRadius, actualRadius);
    }

    [Test]
    public void FindNormaliseVector()
    {
        double expectedNormalisedVectorFirst = 5;
        double actualNormalisedVectorFirst = enemyAI.normaliseVector(3, 4);

        double expectedNormalisedVectorSecond = 10;
        double actualNormalisedVectorSecond = enemyAI.normaliseVector(6, 8);

        double expectedNormalisedVectorThird = 5;
        double actualNormalisedVectorThird = enemyAI.normaliseVector(Mathf.Sqrt(12.5f), Mathf.Sqrt(12.5f));

        bool firstCheck = false;
        bool secondCheck = false;
        bool thirdCheck = false;

        if (Mathf.Abs((float)(expectedNormalisedVectorFirst - actualNormalisedVectorFirst)) < 0.01)
        {
             firstCheck = true;
        }
        if (Mathf.Abs((float)(expectedNormalisedVectorSecond - actualNormalisedVectorSecond)) < 0.01)
        {
            secondCheck = true;
        }
        if (Mathf.Abs((float)(expectedNormalisedVectorThird - actualNormalisedVectorThird)) < 0.01)
        {
            thirdCheck = true;
        }
        Assert.IsTrue(firstCheck);
        Assert.IsTrue(secondCheck);
        Assert.IsTrue(thirdCheck);

    }

    [Test]
    public void FindPopIntMap()
    {
        double x_toTarget = Mathf.Sqrt(12.5f);
        double y_toTarget = Mathf.Sqrt(12.5f);
        bool isObstructed = false;

        bool firstCheck = false;
        bool secondCheck = false;
        bool thirdCheck = false;
        bool fourthCheck = false;
        bool fifthCheck = false;
        bool sixthCheck = false;
        bool seventhCheck = false;
        bool eighthCheck = false;

        double expectedInterestMapZero = 0.70710678118654757;
        double expectedInterestMapFirst = 1;
        double expectedInterestMapSecond = 0.70710678118654757;
        double expectedInterestMapThird = 0;
        double expectedInterestMapFourth = -0.70710678118654757;
        double expectedInterestMapFifth = -1;
        double expectedInterestMapSixth = -0.70710678118654757;
        double expectedInterestMapSeventh = 0;

        enemyAI.populateIntMap(x_toTarget, y_toTarget, 1, isObstructed);
        if (Mathf.Abs((float)(expectedInterestMapZero - enemyAI.interestMap[0])) < 0.05f)
        {
            firstCheck = true;
        }
        if (Mathf.Abs((float)(expectedInterestMapFirst - enemyAI.interestMap[1])) < 0.05f)
        {
            secondCheck = true;
        }
        if (Mathf.Abs((float)(expectedInterestMapSecond - enemyAI.interestMap[2])) < 0.05f)
        {
            thirdCheck = true;
        }
        if (Mathf.Abs((float)(expectedInterestMapThird - enemyAI.interestMap[3])) < 0.05f)
        {
            fourthCheck = true;
        }
        if (Mathf.Abs((float)(expectedInterestMapFourth - enemyAI.interestMap[4])) < 0.05f)
        {
            fifthCheck = true;
        }
        if (Mathf.Abs((float)(expectedInterestMapFifth - enemyAI.interestMap[5])) < 0.05f)
        {
            sixthCheck = true;
        }
        if (Mathf.Abs((float)(expectedInterestMapSixth - enemyAI.interestMap[6])) < 0.05f)
        {
            seventhCheck = true;
        }
        if (Mathf.Abs((float)(expectedInterestMapSeventh - enemyAI.interestMap[7])) < 0.05f)
        {
            eighthCheck = true;
        }

        Assert.IsTrue(firstCheck);
        Assert.IsTrue(secondCheck);
        Assert.IsTrue(thirdCheck);
        Assert.IsTrue(fourthCheck);
        Assert.IsTrue(fifthCheck);
        Assert.IsTrue(sixthCheck);
        Assert.IsTrue(seventhCheck);
        Assert.IsTrue(eighthCheck);
    }
}
