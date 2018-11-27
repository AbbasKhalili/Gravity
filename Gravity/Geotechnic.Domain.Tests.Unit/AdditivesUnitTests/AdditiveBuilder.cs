using Geotechnic.Domain.Additives;

namespace Geotechnic.Domain.Tests.Unit.AdditivesUnitTests
{
    internal class AdditiveBuilder
    {
        private long BranchId { get;  set; }
        private AdditiveId Id { get;  set; }
        private string Title { get;  set; }

        public AdditiveBuilder()
        {
            BranchId = 1000;
            Id = new AdditiveId(100);
        }

        public Additive Build()
        {
            return new Additive(BranchId, Id, Title);
        }

        public AdditiveBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }
    }
}