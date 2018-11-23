using System.Collections.Generic;
using Gravity.Domain;

namespace Geotechnic.Domain.BreakTemplates
{
    public class BreakTemplate : EntityBase<BreakTemplateId>
    {
        public string Title { get; private set; }
        public int MoldCount { get; private set; }
        public IList<BreakTemplateMolds> BreakTemplateMolds { get; private set; }

        protected BreakTemplate() { }

        public BreakTemplate(long branchId, BreakTemplateId id, string title, int moldCount, IList<BreakTemplateMolds> moldList)
        {
            this.Id = id;
            this.BranchId = branchId;
            SetProperties(title, moldCount, moldList);
        }

        public void Update(string title, int moldCount, IList<BreakTemplateMolds> moldList)
        {
            SetProperties(title, moldCount, moldList);
        }

        private void SetProperties(string title, int moldCount, IList<BreakTemplateMolds> moldList)
        {
            Title = title;
            MoldCount = moldCount;
            BreakTemplateMolds = moldList;
        }
    }

    public class BreakTemplateMolds
    {
        public int Age { get; set; }
        public int Count { get; set; }
        public BreakTemplateId BreakTemplateId { get; set; }
    }
}