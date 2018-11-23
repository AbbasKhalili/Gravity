using Gravity.Domain;

namespace Geotechnic.Domain.Additives
{
    public class Additive : EntityBase<AdditiveId>
    {
        public string Title { get; private set; }


        protected Additive() { }
        public Additive(long branchId, AdditiveId id, string title)
        {
            this.BranchId = branchId;
            this.Id = id;
            SetProperties(title);
        }


        public void Update(string title)
        {
            SetProperties(title);
        }

        private void SetProperties(string title)
        {
            Title = title;
        }
    }
}