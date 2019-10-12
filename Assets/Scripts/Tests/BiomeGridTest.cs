using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace Tests
{
    public class BiomeGridTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void BiomeGridTestSimplePasses()
        {
            GridGenerator biomeGridGenerator = new BiomeGridGenerator();
            Assert.False(biomeGridGenerator.IsIncreaseGrid(0));
            Assert.False(biomeGridGenerator.IsIncreaseGrid(-1));
            List<Vector2> terestPositions=biomeGridGenerator.GenerateGridEdgesTest();

            Assert.AreEqual(new Vector2(-1 * biomeGridGenerator.gridBoxSize, -1 * biomeGridGenerator.gridBoxSize),terestPositions[0]);
            Assert.AreEqual(new Vector2(0, biomeGridGenerator.gridBoxSize), terestPositions.Last());
            Assert.True(terestPositions.Contains(new Vector2(-1*biomeGridGenerator.gridBoxSize+biomeGridGenerator.gridBoxSize, -1*biomeGridGenerator.gridBoxSize)));


            Assert.False(biomeGridGenerator.IsIncreaseGrid(10));
            Assert.True(biomeGridGenerator.IsIncreaseGrid(biomeGridGenerator.gridBoxSize* biomeGridGenerator.gridBoxSize+1));



            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator BiomeGridTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
