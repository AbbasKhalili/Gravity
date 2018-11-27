using System;
using Geotechnic.Domain.OrderConcrete;

namespace Geotechnic.Domain.Tests.Unit.OrderConcreteUnitTests
{
    public class BreakBuilder
    {
        //private OrderId OrderId { get; private set; }
        private DateTime BreakDate { get;  set; }
        private int Age { get;  set; }
        private double Length { get;  set; }
        private double Width { get;  set; }
        private double Height { get;  set; }
        private int Weight { get;  set; }
        private int Power { get;  set; }


        public Break Build()
        {
            return new Break(BreakDate,Age,Length,Width,Height,Weight,Power);
        }
        internal BreakBuilder WithPower(int power)
        {
            Power = power;
            return this;
        }
        public BreakBuilder WithWeight(int weight)
        {
            Weight = weight;
            return this;
        }
        public BreakBuilder WithHeight(double height)
        {
            Height = height;
            return this;
        }
        public BreakBuilder WithWidth(double width)
        {
            Width = width;
            return this;
        }
        public BreakBuilder WithLength(double length)
        {
            Length = length;
            return this;
        }
        public BreakBuilder WithAge(int age)
        {
            Age = age;
            return this;
        }
        public BreakBuilder WithBreakDate(DateTime breakDate)
        {
            BreakDate = breakDate;
            return this;
        }
        //public BreakBuilder WithOrderId(OrderId orderId)
        //{
        //    OrderId = orderId;
        //    return this;
        //}
    }
}