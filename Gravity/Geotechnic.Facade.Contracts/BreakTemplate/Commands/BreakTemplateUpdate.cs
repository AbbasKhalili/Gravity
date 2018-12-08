using System.Collections.Generic;

namespace Geotechnic.Facade.Contracts.BreakTemplate.Commands
{
    public class BreakTemplateUpdate
    {
        public long BranchId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public int MoldCount { get; set; }
        public IList<Molds> MoldList { get; set; }

    }
}