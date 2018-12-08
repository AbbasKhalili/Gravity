using System.Collections.Generic;

namespace Geotechnic.Facade.Contracts.BreakTemplate.DTOs
{
    public class BreakTemplateDto
    {
        public long BranchId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public int MoldCount { get; set; }
        public List<BreakTemplateMoldsDto> MoldList { get; set; }
    }
    public class BreakTemplateMoldsDto
    {
        public int Age { get; set; }
        public int Count { get; set; }
        public long BreakTemplateId { get; set; }
    }
}
