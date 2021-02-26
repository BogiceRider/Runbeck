namespace RunBeck.Test
{
    using Moq;
    using NUnit.Framework;
    using RunBeck;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Tests
    {
        private RunBeckFile _file;

        [SetUp]
        public void Setup()
        {
            Mock<IFileExport> fileExport = new Mock<IFileExport>();
            fileExport.Setup(f => f.Export(It.IsAny<string>(), It.IsAny<List<string>>()));
            _file = new RunBeckFile(fileExport.Object);
        }

        [TestCase(3, 2)]
        [TestCase(2, 1)]
        public async Task ShouldProduceIncorrectRecordsUsingTabDelimiter(int numberOfField, int expectRecords)
        {
            _file.Splitter = "\t";
            _file.TotalRecords = new string[] { "First Name,Middle Name,Last Name", "Jane\tTaylor\tDoe", "Chris\tLee", "Jose\tMorro" };
            _file.NumberOfField = numberOfField;

            await _file.Process();

            Assert.AreEqual(expectRecords, _file.IncorrectRecords.Count);
        }

        [TestCase(3, 1)]
        [TestCase(2, 2)]
        public async Task ShouldProduceCorrectRecordsUsingTabDelimiter(int numberOfField, int expectRecords)
        {
            _file.Splitter = "\t";
            _file.TotalRecords = new string[] { "First Name,Middle Name,Last Name", "Jane\tTaylor\tDoe", "Chris\tLee", "Jose\tMorro" };
            _file.NumberOfField = numberOfField;

            await _file.Process();

            Assert.AreEqual(expectRecords, _file.CorrectRecords.Count);
        }

        [TestCase(3, 1)]
        [TestCase(2, 2)]
        [TestCase(4, 3)]
        public async Task ShouldProduceIncorrectRecordsUsingCommaDelimiter(int numberOfField, int expectRecords)
        {
            _file.Splitter = ",";
            _file.TotalRecords = new string[] { "First Name,Middle Name,Last Name", "Jane,Taylor,Doe", "Chris,Lee", "Jose,,Morro" };
            _file.NumberOfField = numberOfField;

            await _file.Process();

            Assert.AreEqual(expectRecords, _file.IncorrectRecords.Count);
        }

        [TestCase(3, 2)]
        [TestCase(2, 1)]
        [TestCase(4, 0)]
        public async Task ShouldProduceCorrectRecordsUsingCommaDelimiter(int numberOfField, int expectRecords)
        {
            _file.Splitter = ",";
            _file.TotalRecords = new string[] { "First Name,Middle Name,Last Name", "Jane,Taylor,Doe", "Chris,Lee", "Jose,,Morro" };
            _file.NumberOfField = numberOfField;

            await _file.Process();

            Assert.AreEqual(expectRecords, _file.CorrectRecords.Count);
        }
    }
}
