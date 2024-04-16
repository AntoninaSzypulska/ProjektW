using Dane;

namespace DaneTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddMethod()
        {
            Kulka kulka = new Kulka(10, 20);

            KulkiRepository repository = new KulkiRepository();

            repository.add(kulka);

            CollectionAssert.Contains(repository.kulkiL, kulka);
        }
    }
}