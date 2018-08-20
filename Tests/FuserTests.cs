using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Codex.Tests
{
    public class FuserTests
    {
        /*[Test]
        public void Constructor_CreateAGameObject()
        {
            var fuser = new Fuser("test");
            Assert.NotNull(fuser.Go);
        }

        [Test]
        public void Add_AddsComponent()
        {
            var fuser = new Fuser("test");
            fuser.Add<Text>();
            Assert.NotNull(fuser.Go.GetComponent<Text>());
        }

        [Test]
        public void Add_AddsGameObject()
        {
            var fuser = new Fuser("test");
            var child = fuser.AddChild("child");
            Assert.AreEqual(1, fuser.Transform.childCount);
        }

        [Test]
        public void Add_AddInHierarchy()
        {
            var fuser = new Fuser("Button")
                .Add<Button>(x => x.interactable = false)
                .AddChild("Label", f => f
                    .Add<Text>(x => x.color = Color.white)
                )
                .Add<Image>(x => x.color = Color.black);
            Assert.NotNull(fuser.Go.GetComponent<Button>());
            Assert.NotNull(fuser.Go.GetComponent<Image>());
            Assert.NotNull(fuser.Transform.GetChild(0).GetComponent<Text>());
        }*/
    }
}