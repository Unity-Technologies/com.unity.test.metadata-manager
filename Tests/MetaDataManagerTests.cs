using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MetaDataManagerTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void MetaDataManagerTestsSimplePasses()
    {
        // Use the Assert class to test conditions
        Assert.True(true);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MetaDataManagerTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
