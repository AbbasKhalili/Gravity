using System.Collections.Generic;
using Geotechnic.Domain.BreakTemplates;

namespace Geotechnic.Domain.Tests.Unit.BreakTemplateUnitTests
{
    internal class BreakTemplateBuilder
    {
        private string Title { get;  set; }
        private int MoldCount { get;  set; }
        private long BranchId { get;  set; }
        private BreakTemplateId Id { get;  set; }
        private List<BreakTemplateMolds> BreakTemplateMolds { get;  set; }

        public BreakTemplateBuilder()
        {
            BranchId = 1000;
            Id = new BreakTemplateId(100);
        }

        public BreakTemplate Build()
        {
            return new BreakTemplate(BranchId,Id,Title,MoldCount,BreakTemplateMolds);
        }

        public BreakTemplateBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }
        public BreakTemplateBuilder WithMoldCount(int count)
        {
            MoldCount = count;
            return this;
        }

        public BreakTemplateBuilder WithMoldsList(List<BreakTemplateMolds> list)
        {
            BreakTemplateMolds = list;
            return this;
        }
    }

    internal class BreakTemplateMoldsBuilder
    {
        private List<BreakTemplateMolds> Result => new List<BreakTemplateMolds>();


        public List<BreakTemplateMolds> Build()
        {
            return Result;
        }

        public BreakTemplateMoldsBuilder WithItem(BreakTemplateId breakTemplateId, int age, int count)
        {
            Result.Add(new BreakTemplateMolds()
            {
                Age = age,
                Count = count,
                BreakTemplateId = breakTemplateId
            });
            return this;
        }
    }
}