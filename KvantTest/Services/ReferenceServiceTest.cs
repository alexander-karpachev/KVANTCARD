using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KvantShared.Model;
using KvantShared.Repos;
using KvantShared.Services;
using KvantShared.Vms.References;
using Xunit;

namespace KvantTest.Services
{
    public class ReferenceServiceTest : AbstractIntegrationTest
    {
        private IGenericRepo<KvantShared.Model.Record> _recordsRepo;
        private IGenericRepo<Reference> _referenceRepo;
        private readonly ReferenceService _referenceService;

        [Fact]
        public void TestReferenceCreation()
        {
            var reference = _referenceService.Create("Simple_1", "Simple Reference #1 И на русском");
            Assert.NotNull(reference);

            var referenceEx = _referenceService.Create< ExtendedRecordClass>("Extended #2", "Расширенный справочник №2");
            Assert.NotNull(reference);
            Assert.Equal(typeof(ExtendedRecordClass), referenceEx.ItemClass);
            Assert.True(referenceEx.Complex);

            referenceEx = _referenceService.AllReferences().FirstOrDefault(e => e.Code == "Extended #2");
            Assert.NotNull(referenceEx);
            Assert.Equal(typeof(ExtendedRecordClass), referenceEx.ItemClass);

            reference.Title = "New Simple name #1";
            _referenceService.Update(reference);
            Assert.Equal("New Simple name #1", reference.Title);

            _referenceService.Remove(reference);
            _referenceService.Remove(referenceEx);

            Assert.Empty(_referenceService.AllReferences());
        }

        [Fact]
        public void TestRecordCreation()
        {
            var referenceEx = _referenceService.Create<ExtendedRecordClass>("Extended #2", "Расширенный справочник №3");
            var record = _referenceService.CreateRecord("ExteNded #2", new ExtendedRecordClass() {Description = "Desc", Title = "Title"});
            Assert.NotNull(record);
            Assert.True(record.Id > 0);

            var record2 = _referenceService.CreateRecord("ExteNded #2", new ExtendedRecordClass() { Description = "Desc #2", Title = "Title #2" });
            Assert.NotNull(record2);
            Assert.True(record2.Id > 0);
            Assert.True(record2.Id != record.Id);

            record.Title = "Updated Title";
            record.Description = "Updated Desc";
            _referenceService.UpdateRecord(record);

            Assert.Equal("Updated Title", record.Title);
            Assert.Equal("Updated Desc", record.Description);

            var records = _referenceService.GetRecords<ExtendedRecordClass>("ExteNdEd #2");
            Assert.Equal(2, records.Count);

            _referenceService.RemoveRecord(record);

            records = _referenceService.GetRecords<ExtendedRecordClass>("EXteNdEd #2");
            Assert.Single(records);

            Assert.Equal(record2.Id, records.First().Id);

            _referenceService.RemoveRecord(record2);
            _referenceService.Remove(referenceEx);
            Assert.Empty(_referenceService.AllReferences());
            Assert.Empty(_referenceService.GetRecords("ExteNdEd #2"));
        }


        public ReferenceServiceTest(TestAppFixture testAppFixture) : base(testAppFixture)
        {
            _referenceService = GetService<ReferenceService>();
            _referenceRepo = GetService<IGenericRepo<Reference>>();
            _recordsRepo = GetService<IGenericRepo<KvantShared.Model.Record>>();
        }

        private class ExtendedRecordClass : BaseRecordVm
        {
            public string Title { get; set; }

            public string  Description { get; set; }

        }
    }
}
