using System.Collections.Generic;

namespace Geotechnic.Facade.Contracts.BreakTemplate.Commands
{
    public class BreakTemplateCreate
    {
        public long BranchId { get; set; }
        public string Title { get; set; }
        public int MoldCount { get; set; }
        public IList<Molds> MoldList { get; set; }
    }
}
