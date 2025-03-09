using System;
using System.Data.SqlTypes;
using Core.Entities;

namespace Core.Specifications;

public class TypeListSpecification : BaseSpecification<Product, string>
{
  public TypeListSpecification()
  {
    AddSelect(x => x.Type);
    ApplyDistinct();
  }
}
