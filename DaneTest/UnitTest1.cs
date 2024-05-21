using Dane;

namespace DaneTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddMethod()
        {
            Kulka kulka = new Kulka(10, 20, 10, 20, 6, 10);

            KulkiRepository repository = new KulkiRepository();

            repository.add(kulka);

            CollectionAssert.Contains(repository.kulkiL, kulka);
        }

        [TestMethod]
        public void TestClearMethod()
        {
            Kulka kulka1 = new Kulka(10, 20, 10, 20, 6, 10);
            Kulka kulka2 = new Kulka(30, 40, 30, 40, 8, 12);
            KulkiRepository repository = new KulkiRepository();

            repository.add(kulka1);
            repository.add(kulka2);

            Assert.AreEqual(2, repository.kulkiL.Count);

            repository.Clear();

            Assert.AreEqual(0, repository.kulkiL.Count);
        }

        [TestMethod]
        public void TestSizeMethod()
        {
            Kulka kulka1 = new Kulka(10, 20, 10, 20, 6, 10);
            Kulka kulka2 = new Kulka(30, 40, 30, 40, 8, 12);
            KulkiRepository repository = new KulkiRepository();

            Assert.AreEqual(0, repository.Size());

            repository.add(kulka1);
            Assert.AreEqual(1, repository.Size());

            repository.add(kulka2);
            Assert.AreEqual(2, repository.Size());

            repository.Clear();
            Assert.AreEqual(0, repository.Size());
        }

        [TestMethod]
        public void TestDistanceToMethod()
        {
            Kulka kulka1 = new Kulka(0, 0, 0, 0, 6, 10);
            Kulka kulka2 = new Kulka(3, 4, 0, 0, 8, 12);

            float expectedDistance = 5.0f;
            float actualDistance = kulka1.DistanceTo(kulka2);

            Assert.AreEqual(expectedDistance, actualDistance);
        }
    }
}