namespace rame.study.sqlperformance.tests
{
    [TestClass]
    public sealed class Test1
    {
        private const string connStrOrigem = "Server=(LocalDB)\\MSSQLLocalDB;Database=rame.study.mock;Trusted_Connection=True;";
        private const string connStrDestino = "Server=(LocalDB)\\MSSQLLocalDB;Database=rame.study.mockcopy;Trusted_Connection=True;";
        private const int countToCopy = 1000;
        private const int waitSeconds = 7;

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange

            var configs = new Configs
            {
                ConnStrDestino = connStrDestino,
                ConnStrOrigem = connStrOrigem,
                CountToCopy = countToCopy,
                WaitSeconds = waitSeconds
            };

            var sqlMethods = new SqlPerformanceMethods(configs);

            // Act

            var result = sqlMethods.PopularBancoOrigem();

            // Assert

            Assert.IsTrue(true);
        }
    }
}