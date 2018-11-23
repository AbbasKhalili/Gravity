using Geotechnic.Domain.Additives;

namespace Geotechnic.Domain.Tests.Unit.AdditivesUnitTests
{
    internal class AdditiveBuilder
    {
        public long BranchId { get; private set; }
        public AdditiveId Id { get; private set; }
        public string Title { get; private set; }

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