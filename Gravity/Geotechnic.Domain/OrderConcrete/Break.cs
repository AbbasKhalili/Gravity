using System;
using Gravity.Core;
using Gravity.Domain;

namespace Geotechnic.Domain.OrderConcrete
{
    public class Break : ValueObjectBase<Break>
    {
        public OrderId OrderId { get; private set; }
        public DateTime BreakDate { get; private set; }
        public int Age { get; private set; }
        public double Length { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public int Weight { get; private set; }
        public int Power { get; private set; }


        protected Break() { }

        public Break(DateTime breakDate, int age, double length, double width, double height, int weight, int power)
        {
            BreakDate = breakDate;
            Age = age;
            Length = length;
            Width = width;
            Height = height;
            Weight = weight;
            Power = power;
        }
        public override bool SameValueAs(Break valueObject)
        {
            return OrderId == valueObject?.OrderId &&
                   BreakDate == valueObject.BreakDate &&
                   Age == valueObject.Age &&
                   Length == valueObject.Length &&
                   Width == valueObject.Width &&
                   Height == valueObject.Height &&
                   Weight == valueObject.Weight && Power == valueObject.Power;
        }

        public override int HashCode()
        {
            return new HashCodeBuilder().Append(OrderId).Append(BreakDate)
                .Append(Age).Append(Length).Append(Width).Append(Height)
                .Append(Weight).Append(Power).ToHashCode();
        }
    }
}
