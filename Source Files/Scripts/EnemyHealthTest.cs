using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyHealthTest
{
    private EnemyHealth enemyHealth;
    private Animator animator;
    public RuntimeAnimatorController mockedAnimatorController;

    [SetUp]
    public void Setup()
    {
        
        GameObject enemyObject = new GameObject();
        enemyHealth = enemyObject.AddComponent<EnemyHealth>();
        animator = enemyObject.AddComponent<Animator>();
        mockedAnimatorController = new AnimatorController(); // Create a mocked or fake animation controller
        animator.runtimeAnimatorController = mockedAnimatorController;

        enemyHealth.animator = animator;
        enemyHealth.Health = 10;
        
    }

    [Test]
    public void OnHit_ReduceHealth()
    {
        // Arrange
        float initialHealth = 10f;
        float damage = 2f;

        // Act
        enemyHealth.OnHit(damage);

        // Assert
        float expectedHealth = initialHealth - damage;
        Assert.AreEqual(expectedHealth, enemyHealth.Health);
    }
}
